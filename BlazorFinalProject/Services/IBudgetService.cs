namespace BlazorFinalProject.Services;

public interface IBudgetService
{
    Task<decimal> GetLimitAsync();
    Task SetLimitAsync(decimal limit);
}