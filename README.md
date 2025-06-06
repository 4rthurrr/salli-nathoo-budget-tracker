# Budget Tracker (සල්ලි නැතෝ) 💰

A modern, responsive budget tracking web application built with ASP.NET Core MVC and Google OAuth 2.0 authentication.

![Budget Tracker](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-purple)
![OAuth](https://img.shields.io/badge/OAuth-2.0-orange)

## 🚀 Features

### ✅ Authentication & Security
- **Google OAuth 2.0** integration for secure login
- Local account registration and login support
- User-specific data isolation
- Secure session management

### 📊 Budget Management
- **Income Tracking**: Record salary, freelance, investments, and other income sources
- **Expense Tracking**: Categorize spending across food, rent, utilities, entertainment, travel, and more
- **CRUD Operations**: Full create, read, update, delete functionality for transactions
- **Category Management**: Predefined categories with the ability to expand

### 📈 Dashboard & Reports
- **Visual Dashboard**: Monthly income/expense summaries with net balance
- **Interactive Charts**: Monthly trends and expense breakdowns using Chart.js
- **Recent Transactions**: Quick overview of latest financial activity
- **Monthly Reports**: Detailed month-by-month analysis
- **CSV Export**: Download your financial data for external analysis

### 🎨 User Experience
- **Responsive Design**: Works perfectly on desktop, tablet, and mobile
- **Modern UI**: Bootstrap 5 with custom styling and Font Awesome icons
- **Intuitive Navigation**: Easy-to-use interface for all skill levels
- **Real-time Validation**: Client and server-side form validation

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: SQLite (development) / SQL Server (production ready)
- **ORM**: Entity Framework Core 8.0
- **Authentication**: ASP.NET Core Identity + Google OAuth 2.0
- **Frontend**: Bootstrap 5.3, jQuery, Chart.js
- **Icons**: Font Awesome 6.0

## 📁 Project Structure

```
BudgetTracker/
├── Controllers/           # MVC Controllers
│   ├── DashboardController.cs
│   ├── TransactionsController.cs
│   ├── ReportsController.cs
│   └── HomeController.cs
├── Models/               # Data Models
│   ├── ApplicationUser.cs
│   ├── Transaction.cs
│   ├── Category.cs
│   └── ViewModels/
├── Views/                # Razor Views
│   ├── Dashboard/
│   ├── Transactions/
│   ├── Reports/
│   ├── Home/
│   └── Shared/
├── Data/                 # Database Context
│   └── ApplicationDbContext.cs
├── Areas/Identity/       # Authentication Pages
├── wwwroot/             # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
└── appsettings.json     # Configuration
```

## 🔧 Setup Instructions

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Google OAuth Setup
1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select existing one
3. Enable Google+ API
4. Create OAuth 2.0 credentials:
   - Application type: Web application
   - Authorized redirect URIs: `https://localhost:7xxx/signin-google`
5. Copy Client ID and Client Secret

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/budget-tracker.git
   cd budget-tracker
   ```

2. **Setup Environment Variables**
   ```bash
   # Copy the environment template
   cp .env.example .env
   
   # Edit .env and add your actual secrets
   # See ENV_SETUP_GUIDE.md for detailed instructions
   ```

3. **Configure Google OAuth**
   
   Update your `.env` file with Google OAuth credentials:
   ```bash
   Authentication__Google__ClientId=your-google-client-id.googleusercontent.com
   Authentication__Google__ClientSecret=your-google-client-secret
   ```
   
   Or alternatively update `appsettings.json`:
   ```json
   {
     "Authentication": {
       "Google": {
         "ClientId": "YOUR_GOOGLE_CLIENT_ID",
         "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET"
       }
     }
   }
   ```

4. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the Application**
   - Open browser and navigate to `https://localhost:7xxx`
   - Login with Google or create a local account
   - Start tracking your budget!

## 💡 Usage

### Adding Transactions
1. Click "Add Transaction" from dashboard or navigation
2. Select Income or Expense type
3. Choose appropriate category
4. Enter amount and description
5. Set the date and save

### Viewing Reports
- **Dashboard**: Overview of current month's financial status
- **Reports**: Detailed analysis with charts and export options
- **Monthly View**: Month-specific breakdowns

### Exporting Data
- Go to Reports section
- Click "Export to CSV" for Excel-compatible format

## 🎯 Key Features for CV/Portfolio

This project demonstrates:

✅ **Full-Stack Development**: Complete MVC architecture implementation  
✅ **Modern Authentication**: OAuth 2.0 integration with external providers  
✅ **Database Design**: Proper Entity Framework relationships and migrations  
✅ **Security Best Practices**: User data isolation and input validation  
✅ **Responsive Design**: Mobile-first Bootstrap implementation  
✅ **Data Visualization**: Interactive charts and reporting  
✅ **Clean Code**: SOLID principles and proper project structure  
✅ **Version Control**: Git workflow and documentation  

## 🚀 Deployment Options

### Azure App Service
1. Create Azure App Service
2. Configure connection string for Azure SQL
3. Set Google OAuth credentials in Application Settings
4. Deploy via Visual Studio or GitHub Actions

### Railway/Render
1. Connect GitHub repository
2. Set environment variables
3. Configure build settings
4. Deploy automatically on push

## 🔮 Future Enhancements

- [ ] **Budget Goals**: Set and track monthly budget limits
- [ ] **Recurring Transactions**: Automate regular income/expenses
- [ ] **Multiple Currencies**: Support for different currencies
- [ ] **Bank Integration**: Connect to bank APIs for automatic import
- [ ] **Mobile App**: React Native or Flutter companion app
- [ ] **Advanced Reports**: Yearly summaries and financial insights
- [ ] **Expense Splitting**: Share expenses with family/roommates
- [ ] **Receipt Scanning**: OCR for automatic expense entry

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📧 Contact

**Your Name** - [your.email@example.com](mailto:your.email@example.com)

Project Link: [https://github.com/yourusername/budget-tracker](https://github.com/yourusername/budget-tracker)

---

*Built with ❤️ using ASP.NET Core and modern web technologies*
