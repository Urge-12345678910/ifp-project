using BlazorFinalProject.Data;
using BlazorFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFinalProject.Services;

public class BudgetService : IBudgetService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public BudgetService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<decimal> GetLimitAsync()
    {
        await using var db = _factory.CreateDbContext();
        var budget = await db.Budgets.OrderBy(b => b.Id).FirstOrDefaultAsync();
        return budget?.MonthlyLimit ?? 0;
    }

    public async Task SetLimitAsync(decimal limit)
    {
        await using var db = _factory.CreateDbContext();
        var budget = await db.Budgets.OrderBy(b => b.Id).FirstOrDefaultAsync();
        if (budget is null)
            db.Budgets.Add(new Budget { MonthlyLimit = limit });
        else
            budget.MonthlyLimit = limit;
        await db.SaveChangesAsync();
    }
}