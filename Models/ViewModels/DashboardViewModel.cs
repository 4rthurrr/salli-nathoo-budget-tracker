namespace BudgetTracker.Models.ViewModels;

public class DashboardViewModel
{
    public decimal CurrentMonthIncome { get; set; }
    public decimal CurrentMonthExpenses { get; set; }
    public decimal NetBalance => CurrentMonthIncome - CurrentMonthExpenses;
    public List<Transaction> RecentTransactions { get; set; } = new();
    public List<CategorySummary> ExpensesByCategory { get; set; } = new();
    public List<MonthlyData> MonthlyData { get; set; } = new();
}

public class CategorySummary
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Color { get; set; } = string.Empty;
}

public class MonthlyData
{
    public string Month { get; set; } = string.Empty;
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
}
