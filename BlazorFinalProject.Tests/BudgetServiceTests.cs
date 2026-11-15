using BlazorFinalProject.Data;
using BlazorFinalProject.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlazorFinalProject.Tests;

public class BudgetServiceTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly BudgetService _service;

    public BudgetServiceTests()
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

        _service = new BudgetService(factory);
    }

    public void Dispose() => _connection.Dispose();

    [Fact]
    public async Task GetLimitAsync_ReturnsZero_WhenNotSet()
    {
        Assert.Equal(0, await _service.GetLimitAsync());
    }

    [Fact]
    public async Task SetLimitAsync_SavesLimit()
    {
        await _service.SetLimitAsync(500);

        Assert.Equal(500, await _service.GetLimitAsync());
    }

    [Fact]
    public async Task SetLimitAsync_UpdatesExistingLimit()
    {
        await _service.SetLimitAsync(500);
        await _service.SetLimitAsync(800);

        Assert.Equal(800, await _service.GetLimitAsync());
    }
}
