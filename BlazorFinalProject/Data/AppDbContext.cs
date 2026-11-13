using BlazorFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFinalProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Budget> Budgets => Set<Budget>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Expense>()
        .HasOne(e => e.Category)
        .WithMany()
        .HasForeignKey(e => e.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "food", Color = "#28a745" },
            new Category { Id = 2, Name = "vehicle", Color = "#007bff" },
            new Category { Id = 3, Name = "apartment", Color = "#ffc107" },
            new Category { Id = 4, Name = "enterteiment", Color = "#dc3545" });
    }
}