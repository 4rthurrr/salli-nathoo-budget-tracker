using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Controllers;

[Authorize]
public class TransactionsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public TransactionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var userId = _userManager.GetUserId(User);
        
        var totalTransactions = await _context.Transactions
            .Where(t => t.UserId == userId)
            .CountAsync();

        var transactions = await _context.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalTransactions / pageSize);
        ViewBag.PageSize = pageSize;

        return View(transactions);
    }

    public async Task<IActionResult> Create()
    {
        var viewModel = new TransactionViewModel();
        await PopulateCategoriesAsync(viewModel, TransactionType.Expense);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            var transaction = new Transaction
            {
                Amount = model.Amount,
                Description = model.Description,
                Date = model.Date,
                Type = model.Type,
                CategoryId = model.CategoryId,
                UserId = userId!
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Transaction added successfully!";
            return RedirectToAction(nameof(Index));
        }

        await PopulateCategoriesAsync(model, model.Type);
        return View(model);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = _userManager.GetUserId(User);
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (transaction == null)
        {
            return NotFound();
        }

        var viewModel = new TransactionViewModel
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Date = transaction.Date,
            Type = transaction.Type,
            CategoryId = transaction.CategoryId
        };

        await PopulateCategoriesAsync(viewModel, transaction.Type);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TransactionViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var transaction = await _context.Transactions
                    .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

                if (transaction == null)
                {
                    return NotFound();
                }

                transaction.Amount = model.Amount;
                transaction.Description = model.Description;
                transaction.Date = model.Date;
                transaction.Type = model.Type;
                transaction.CategoryId = model.CategoryId;

                _context.Update(transaction);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Transaction updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransactionExistsAsync(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        await PopulateCategoriesAsync(model, model.Type);
        return View(model);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = _userManager.GetUserId(User);
        var transaction = await _context.Transactions
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = _userManager.GetUserId(User);
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Transaction deleted successfully!";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesByType(TransactionType type)
    {
        var categories = await _context.Categories
            .Where(c => c.Type == type)
            .Select(c => new { value = c.Id, text = c.Name })
            .ToListAsync();

        return Json(categories);
    }

    private async Task PopulateCategoriesAsync(TransactionViewModel model, TransactionType type)
    {
        var categories = await _context.Categories
            .Where(c => c.Type == type)
            .ToListAsync();

        model.Categories = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
    }

    private async Task<bool> TransactionExistsAsync(int id)
    {
        var userId = _userManager.GetUserId(User);
        return await _context.Transactions
            .AnyAsync(e => e.Id == id && e.UserId == userId);
    }
}
