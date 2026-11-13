using BlazorFinalProject.Data;
using BlazorFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFinalProject.Services;

public class ExpenseService : IExpenseService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly ILogger<ExpenseService> _logger;

    public ExpenseService(IDbContextFactory<AppDbContext> factory, ILogger<ExpenseService> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<List<Expense>> GetAllAsync(int? categoryId = null, string? search = null)
    {
        await using var db = _factory.CreateDbContext();
        var query = db.Expenses.Include(e => e.Category).AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(e => e.CategoryId == categoryId.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.Title.Contains(search));

        return await query.OrderByDescending(e => e.Date).ToListAsync();
    }

    public async Task<Expense?> GetByIdAsync(int id)
    {
        await using var db = _factory.CreateDbContext();
        return await db.Expenses.FindAsync(id);
    }

     private async Task SaveAsync(Func<AppDbContext, Task> action)
    {
        try
        {
            await using var db = _factory.CreateDbContext();
            await action(db);
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database save failed");
            throw new InvalidOperationException("Could not save changes. Please try again.");
        }
    }

    public Task AddAsync(Expense expense) =>
        SaveAsync(db => { db.Expenses.Add(expense); return Task.CompletedTask; });

    public Task UpdateAsync(Expense expense) =>
        SaveAsync(db => { db.Expenses.Update(expense); return Task.CompletedTask; });

    public Task DeleteAsync(int id) =>
        SaveAsync(async db =>
        {
            var expense = await db.Expenses.FindAsync(id);
            if (expense is not null) db.Expenses.Remove(expense);
        });

    public async Task<decimal> GetMonthTotalAsync(DateTime month)
    {
        await using var db = _factory.CreateDbContext();
        var amounts = await db.Expenses
            .Where(e => e.Date.Year == month.Year && e.Date.Month == month.Month)
            .Select(e => e.Amount)
            .ToListAsync();
        return amounts.Sum();
    }

    public async Task<List<CategoryTotal>> GetTotalsByCategoryAsync()
    {
        await using var db = _factory.CreateDbContext();
        var items = await db.Expenses.Include(e => e.Category).ToListAsync();
        return items
            .GroupBy(e => e.Category!)
            .Select(g => new CategoryTotal(g.Key.Name, g.Key.Color, g.Sum(e => e.Amount)))
            .OrderByDescending(t => t.Total)
            .ToList();
    }
}