# Google OAuth Setup Guide

To enable Google OAuth authentication in your Budget Tracker application, follow these steps:

## 1. Create Google OAuth Credentials

1. Go to the [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Enable the Google+ API
4. Go to "Credentials" in the left sidebar
5. Click "Create Credentials" → "OAuth 2.0 Client IDs"
6. Configure the OAuth consent screen if prompted
7. Choose "Web application" as the application type
8. Add authorized redirect URIs:
   - For development: `https://localhost:5001/signin-google`
   - For production: `https://yourdomain.com/signin-google`

## 2. Update Configuration Files

### For Development (appsettings.Development.json):
```json
{
  "Authentication": {
    "Google": {
      "ClientId": "your-actual-google-client-id.googleusercontent.com",
      "ClientSecret": "your-actual-google-client-secret"
    }
  }
}
```

### For Production (appsettings.json):
```json
{
  "Authentication": {
    "Google": {
      "ClientId": "your-production-google-client-id.googleusercontent.com",
      "ClientSecret": "your-production-google-client-secret"
    }
  }
}
```

## 3. Security Notes

- **Never commit real credentials to version control**
- Keep the appsettings files with placeholder values in your repository
- Use environment variables or Azure Key Vault for production secrets
- The current configuration automatically disables Google OAuth if placeholder values are detected

## 4. Testing

1. Replace the placeholder values in `appsettings.Development.json`
2. Restart the application
3. The "Login with Google" button should now work
4. Users can also create local accounts using the regular registration form

## 5. Alternative: User Secrets (Recommended for Development)

Instead of modifying appsettings files, use .NET User Secrets:

```bash
dotnet user-secrets set "Authentication:Google:ClientId" "your-client-id"
dotnet user-secrets set "Authentication:Google:ClientSecret" "your-client-secret"
```

This keeps your credentials secure and separate from your code.
