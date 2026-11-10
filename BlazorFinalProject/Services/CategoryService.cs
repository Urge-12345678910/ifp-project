using BlazorFinalProject.Data;
using BlazorFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFinalProject.Services;

public class CategoryService : ICategoryService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public CategoryService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        await using var db = _factory.CreateDbContext();
        return await db.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await using var db = _factory.CreateDbContext();
        db.Categories.Add(category);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var db = _factory.CreateDbContext();
        var category = await db.Categories.FindAsync(id);
        if (category is null) return;
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
    }
}