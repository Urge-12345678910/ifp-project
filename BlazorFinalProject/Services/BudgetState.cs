namespace BlazorFinalProject.Services;

public class BudgetState
{
    private readonly IExpenseService _expenses;
    private readonly IBudgetService _budget;

    public decimal Spent { get; private set; }
    public decimal Limit { get; private set; }
    public event Action? OnChange;

    public BudgetState(IExpenseService expenses, IBudgetService budget)
    {
        _expenses = expenses;
        _budget = budget;
    }

    public async Task RefreshAsync()
    {
        Spent = await _expenses.GetMonthTotalAsync(DateTime.Today);
        Limit = await _budget.GetLimitAsync();
        OnChange?.Invoke();
    }

    public async Task SetLimitAsync(decimal limit)
    {
        await _budget.SetLimitAsync(limit);
        await RefreshAsync();
    }
}