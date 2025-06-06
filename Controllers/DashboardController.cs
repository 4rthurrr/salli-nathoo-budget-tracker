using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var currentMonth = DateTime.Now;
        var startOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        // Get current month's transactions
        var currentMonthTransactions = await _context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId && t.Date >= startOfMonth && t.Date <= endOfMonth)
            .ToListAsync();

        // Get recent transactions (last 10)
        var recentTransactions = await _context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .Take(10)
            .ToListAsync();

        // Calculate monthly totals
        var monthlyIncome = currentMonthTransactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var monthlyExpenses = currentMonthTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        // Get expenses by category for current month
        var expensesByCategory = currentMonthTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.Category.Name)
            .Select(g => new CategorySummary
            {
                CategoryName = g.Key,
                Amount = g.Sum(t => t.Amount),
                Color = GetRandomColor()
            })
            .OrderByDescending(c => c.Amount)
            .ToList();

        // Get last 6 months data
        var monthlyData = new List<MonthlyData>();
        for (int i = 5; i >= 0; i--)
        {
            var month = DateTime.Now.AddMonths(-i);
            var monthStart = new DateTime(month.Year, month.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var monthTransactions = await _context.Transactions
                .Where(t => t.UserId == userId && t.Date >= monthStart && t.Date <= monthEnd)
                .ToListAsync();

            monthlyData.Add(new MonthlyData
            {
                Month = month.ToString("MMM yyyy"),
                Income = monthTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                Expenses = monthTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
            });
        }

        var viewModel = new DashboardViewModel
        {
            CurrentMonthIncome = monthlyIncome,
            CurrentMonthExpenses = monthlyExpenses,
            RecentTransactions = recentTransactions,
            ExpensesByCategory = expensesByCategory,
            MonthlyData = monthlyData
        };

        return View(viewModel);
    }

    private static string GetRandomColor()
    {
        var colors = new[]
        {
            "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF",
            "#FF9F40", "#FF6384", "#C9CBCF", "#4BC0C0", "#FF6384"
        };
        var random = new Random();
        return colors[random.Next(colors.Length)];
    }
}
