using Microsoft.AspNetCore.Identity;

namespace BudgetTracker.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
