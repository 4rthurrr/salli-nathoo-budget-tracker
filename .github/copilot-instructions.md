<!-- Use this file to provide workspace-specific custom instructions to Copilot. For more details, visit https://code.visualstudio.com/docs/copilot/copilot-customization#_use-a-githubcopilotinstructionsmd-file -->

# Budget Tracker (සල්ලි නැතෝ) - Copilot Instructions

This is an ASP.NET Core MVC web application for budget tracking with the following key characteristics:

## Project Context
- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: Entity Framework Core with SQLite (development)
- **Authentication**: ASP.NET Core Identity + Google OAuth 2.0
- **UI Framework**: Bootstrap 5.3 with responsive design
- **Charts**: Chart.js for data visualization
- **Language**: C# with Razor views

## Architecture Patterns
- Follow MVC pattern strictly
- Use Repository pattern through Entity Framework
- Implement proper dependency injection
- Apply separation of concerns

## Code Conventions
- Use async/await for all database operations
- Implement proper error handling and validation
- Follow RESTful conventions for controllers
- Use ViewModels for complex views
- Apply proper authorization attributes

## Database Schema
- ApplicationUser (extends IdentityUser)
- Transaction (Income/Expense with categories)
- Category (Predefined categories for transactions)
- Proper foreign key relationships and constraints

## Security Considerations
- All financial data must be user-specific
- Implement proper authorization checks
- Validate all user inputs
- Use HTTPS for all operations
- Secure OAuth 2.0 implementation

## UI/UX Guidelines
- Mobile-first responsive design
- Use Bootstrap components consistently
- Implement proper form validation
- Show appropriate success/error messages
- Use Font Awesome icons consistently

## Features to Maintain
- Dashboard with charts and summaries
- CRUD operations for transactions
- Monthly/yearly reporting
- CSV export functionality
- Google OAuth integration
- Category-based expense tracking

When suggesting code changes or new features, ensure they align with these architectural decisions and maintain the existing code quality standards.
