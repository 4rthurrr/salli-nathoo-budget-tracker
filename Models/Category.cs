using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public TransactionType Type { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
