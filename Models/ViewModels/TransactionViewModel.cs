using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.ViewModels;

public class TransactionViewModel
{
    public int Id { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now;
    
    [Required]
    public TransactionType Type { get; set; }
    
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    
    public List<SelectListItem> Categories { get; set; } = new();
}
