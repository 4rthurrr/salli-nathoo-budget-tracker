using BudgetTracker.Data;
using BudgetTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BudgetTracker.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        
        // Get all transactions for the user
        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        return View(transactions);
    }

    public async Task<IActionResult> ExportToCsv()
    {
        var userId = _userManager.GetUserId(User);
        
        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("Date,Type,Category,Description,Amount");

        foreach (var transaction in transactions)
        {
            csv.AppendLine($"{transaction.Date:yyyy-MM-dd},{transaction.Type},{transaction.Category.Name},\"{transaction.Description}\",{transaction.Amount}");
        }

        var bytes = Encoding.UTF8.GetBytes(csv.ToString());
        return File(bytes, "text/csv", $"budget-report-{DateTime.Now:yyyy-MM-dd}.csv");
    }

    public async Task<IActionResult> Monthly(int year = 0, int month = 0)
    {
        if (year == 0) year = DateTime.Now.Year;
        if (month == 0) month = DateTime.Now.Month;

        var userId = _userManager.GetUserId(User);
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId && t.Date >= startDate && t.Date <= endDate)
            .ToListAsync();

        ViewBag.Year = year;
        ViewBag.Month = month;
        ViewBag.MonthName = startDate.ToString("MMMM yyyy");

        return View(transactions);
    }
}
