using BlazorFinalProject.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorFinalProject.Tests;

public class TestDbFactory : IDbContextFactory<AppDbContext>
{
    private readonly DbContextOptions<AppDbContext> _options;

    public TestDbFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public AppDbContext CreateDbContext() => new AppDbContext(_options);
}