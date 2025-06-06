using BudgetTracker.Data;
using BudgetTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Data Source=BudgetTracker.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add Google OAuth authentication (only if credentials are configured)
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

if (!string.IsNullOrEmpty(googleClientId) && 
    !string.IsNullOrEmpty(googleClientSecret) &&
    googleClientId != "YOUR_GOOGLE_CLIENT_ID" &&
    googleClientSecret != "YOUR_GOOGLE_CLIENT_SECRET" &&
    googleClientId != "YOUR_DEVELOPMENT_GOOGLE_CLIENT_ID" &&
    googleClientSecret != "YOUR_DEVELOPMENT_GOOGLE_CLIENT_SECRET")
{
    builder.Services.AddAuthentication()
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = googleClientId;
            googleOptions.ClientSecret = googleClientSecret;
        });
}

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    
    // Seed default categories
    if (!context.Categories.Any())
    {
        var categories = new[]
        {
            new Category { Name = "Food", Type = TransactionType.Expense },
            new Category { Name = "Rent", Type = TransactionType.Expense },
            new Category { Name = "Utilities", Type = TransactionType.Expense },
            new Category { Name = "Entertainment", Type = TransactionType.Expense },
            new Category { Name = "Travel", Type = TransactionType.Expense },
            new Category { Name = "Miscellaneous", Type = TransactionType.Expense },
            new Category { Name = "Salary", Type = TransactionType.Income },
            new Category { Name = "Freelance", Type = TransactionType.Income },
            new Category { Name = "Investment", Type = TransactionType.Income },
            new Category { Name = "Other Income", Type = TransactionType.Income }
        };
        
        context.Categories.AddRange(categories);
        context.SaveChanges();
    }
}

app.Run();
