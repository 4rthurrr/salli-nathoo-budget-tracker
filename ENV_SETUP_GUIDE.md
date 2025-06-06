# Environment Variables Setup Guide

This guide explains how to use the `.env` file to securely manage your application's secret keys and configuration.

## 🔧 Setup Instructions

### 1. Copy the Template
```bash
cp .env.example .env
```

### 2. Edit the `.env` File
Open the `.env` file and replace the placeholder values with your actual secrets:

```bash
# Example: Replace placeholder with real Google OAuth credentials
Authentication__Google__ClientId=your-actual-client-id.googleusercontent.com
Authentication__Google__ClientSecret=GOCSPX-your_actual_client_secret_here
```

## 🔐 Security Best Practices

### ✅ DO:
- Keep `.env` files local to your development machine
- Use different `.env` files for different environments
- Regularly rotate your secrets
- Use strong, unique values for each secret
- Share the `.env.example` file with your team

### ❌ DON'T:
- **NEVER** commit `.env` files to version control
- Don't share `.env` files via email or chat
- Don't reuse the same secrets across multiple applications
- Don't use weak or predictable secret values

## 📁 File Structure

```
├── .env                 # Your actual secrets (git-ignored)
├── .env.example        # Template file (safe to commit)
├── .gitignore          # Ensures .env is not committed
└── Program.cs          # Loads .env automatically
```

## 🌍 Environment Variables Explained

### Database Configuration
```bash
ConnectionStrings__DefaultConnection=Data Source=BudgetTracker.db
```

### Google OAuth 2.0
```bash
# Get these from Google Cloud Console
Authentication__Google__ClientId=your-client-id.googleusercontent.com
Authentication__Google__ClientSecret=your-client-secret
```

### Security Keys
```bash
# Use a random 32+ character string
Jwt__Key=your-super-secret-jwt-key-minimum-32-characters-long
DataProtection__Key=your-data-protection-key-32-chars-min
```

### Email Configuration
```bash
# For sending password reset emails, notifications, etc.
Email__SmtpServer=smtp.gmail.com
Email__SmtpPort=587
Email__Username=your-email@gmail.com
Email__Password=your-app-specific-password
```

## 🔑 How to Get Google OAuth Credentials

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create or select a project
3. Enable the Google+ API
4. Go to "Credentials" → "Create Credentials" → "OAuth 2.0 Client IDs"
5. Configure OAuth consent screen
6. Set authorized redirect URIs:
   - Development: `https://localhost:5001/signin-google`
   - Production: `https://yourdomain.com/signin-google`
7. Copy the Client ID and Client Secret to your `.env` file

## 🚀 Production Deployment

For production, use your hosting platform's environment variable system:

### Azure App Service
```bash
az webapp config appsettings set --name myapp --resource-group mygroup --settings "Authentication__Google__ClientId=your-value"
```

### Docker
```dockerfile
ENV Authentication__Google__ClientId=your-value
ENV Authentication__Google__ClientSecret=your-value
```

### Environment Variables
```bash
export Authentication__Google__ClientId=your-value
export Authentication__Google__ClientSecret=your-value
```

## 🧪 Testing the Setup

1. Add your Google OAuth credentials to `.env`
2. Restart the application: `dotnet run`
3. Navigate to the login page
4. The "Login with Google" button should now work

## 🔍 Troubleshooting

### Issue: Google OAuth not working
- Check that your credentials are correct in `.env`
- Verify redirect URIs match in Google Console
- Ensure the application is restarted after changing `.env`

### Issue: .env file not loading
- Verify `DotNetEnv` package is installed
- Check that `Env.Load()` is called in `Program.cs`
- Ensure `.env` file is in the root directory

### Issue: Database connection problems
- Check the `ConnectionStrings__DefaultConnection` value
- Ensure the database file path is correct
- Run `dotnet ef database update` if needed

## 📝 Notes

- The `.env` file is automatically loaded when the application starts
- Environment variables override appsettings.json values
- Use double underscores (`__`) to represent nested configuration sections
- The application will work with placeholder values (Google OAuth will be disabled)
