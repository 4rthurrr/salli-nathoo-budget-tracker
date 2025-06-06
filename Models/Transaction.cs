using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models;

public enum TransactionType
{
    Income,
    Expense
}

public class Transaction
{
    public int Id { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    
    [Required]
    public TransactionType Type { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
