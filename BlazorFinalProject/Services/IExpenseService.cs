using BlazorFinalProject.Models;

namespace BlazorFinalProject.Services;

public interface IExpenseService
{
    Task<List<Expense>> GetAllAsync(int? categoryId = null, string? search = null);
    Task<Expense?> GetByIdAsync(int id);
    Task AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(int id);
    Task<decimal> GetMonthTotalAsync(DateTime month);
    Task<List<CategoryTotal>> GetTotalsByCategoryAsync();
}