using BlazorFinalProject.Models;

namespace BlazorFinalProject.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task AddAsync(Category category);
    Task DeleteAsync(int id);
}