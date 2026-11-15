using BlazorFinalProject.Data;
using BlazorFinalProject.Models;
using BlazorFinalProject.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace BlazorFinalProject.Tests;

public class ExpenseServiceTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ExpenseService _service;

    public ExpenseServiceTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        var factory = new TestDbFactory(options);
        using (var db = factory.CreateDbContext())
        {
            db.Database.EnsureCreated();
        }

        _service = new ExpenseService(factory, NullLogger<ExpenseService>.Instance);
    }

    public void Dispose() => _connection.Dispose();

    private static Expense Make(string title, decimal amount, int categoryId, DateTime date) =>
        new() { Title = title, Amount = amount, CategoryId = categoryId, Date = date };

    [Fact]
    public async Task AddAsync_SavesExpense()
    {
        await _service.AddAsync(Make("Bread", 5, 1, DateTime.Today));

        var all = await _service.GetAllAsync();

        Assert.Single(all);
        Assert.Equal("Bread", all[0].Title);
    }

    [Fact]
    public async Task GetAllAsync_FiltersByCategory()
    {
        await _service.AddAsync(Make("Bread", 5, 1, DateTime.Today));
        await _service.AddAsync(Make("Bus", 2, 2, DateTime.Today));

        var result = await _service.GetAllAsync(categoryId: 2);

        Assert.Single(result);
        Assert.Equal("Bus", result[0].Title);
    }

    [Fact]
    public async Task GetAllAsync_SearchesByTitle()
    {
        await _service.AddAsync(Make("Bread", 5, 1, DateTime.Today));
        await _service.AddAsync(Make("Bus", 2, 2, DateTime.Today));

        var result = await _service.GetAllAsync(search: "Bre");

        Assert.Single(result);
    }

    [Fact]
    public async Task UpdateAsync_ChangesAmount()
    {
        await _service.AddAsync(Make("Bread", 5, 1, DateTime.Today));
        var expense = (await _service.GetAllAsync())[0];
        expense.Amount = 9;

        await _service.UpdateAsync(expense);

        var updated = await _service.GetByIdAsync(expense.Id);
        Assert.Equal(9, updated!.Amount);
    }

    [Fact]
    public async Task DeleteAsync_RemovesExpense()
    {
        await _service.AddAsync(Make("Bread", 5, 1, DateTime.Today));
        var id = (await _service.GetAllAsync())[0].Id;

        await _service.DeleteAsync(id);

        Assert.Empty(await _service.GetAllAsync());
    }

    [Fact]
    public async Task GetMonthTotalAsync_SumsOnlyThatMonth()
    {
        await _service.AddAsync(Make("A", 10, 1, DateTime.Today));
        await _service.AddAsync(Make("B", 15, 1, DateTime.Today));
        await _service.AddAsync(Make("Old", 100, 1, DateTime.Today.AddMonths(-2)));

        var total = await _service.GetMonthTotalAsync(DateTime.Today);

        Assert.Equal(25, total);
    }

    [Fact]
    public async Task GetTotalsByCategoryAsync_GroupsByCategory()
    {
        await _service.AddAsync(Make("A", 10, 1, DateTime.Today));
        await _service.AddAsync(Make("B", 20, 1, DateTime.Today));
        await _service.AddAsync(Make("C", 5, 2, DateTime.Today));

        var totals = await _service.GetTotalsByCategoryAsync();

        Assert.Equal(2, totals.Count);
        Assert.Equal(30, totals[0].Total);
    }
}