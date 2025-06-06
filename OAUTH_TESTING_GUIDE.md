# Google OAuth Testing Guide

🎉 **Your Google OAuth credentials have been successfully added!**

## ✅ Current Setup Status

### Environment Variables Loaded:
- ✅ **Client ID**: `[YOUR_GOOGLE_CLIENT_ID]` (loaded from .env)
- ✅ **Client Secret**: `[YOUR_GOOGLE_CLIENT_SECRET]` (loaded from .env)
- ✅ **DotNetEnv Package**: Installed and configured
- ✅ **Application**: Built successfully

> **Note**: Actual credentials are stored securely in the `.env` file (not tracked by git)

## 🔧 Testing Instructions

### 1. Access the Application
- **URL**: https://localhost:5001 or http://localhost:5000
- **Status**: Application should be running

### 2. Test Google OAuth Login
1. Navigate to the home page
2. Click on **"Login"** or **"Login with Google"** button
3. You should be redirected to Google's authentication page
4. Sign in with your Google account
5. Grant permissions to the Budget Tracker application
6. You should be redirected back to the application dashboard

### 3. Test Local Account Registration (Alternative)
If you prefer not to use Google OAuth:
1. Click on **"Register"** 
2. Create a local account with email/password
3. Log in with your local credentials

## 🚀 Expected Behavior

### ✅ With Valid Credentials (Current State):
- Google OAuth login button should be visible and functional
- Clicking it should redirect to Google's authentication page
- After successful authentication, user should be logged into the application
- Dashboard should display with user-specific data

### ❌ Previous State (Placeholder Credentials):
- Google OAuth was automatically disabled
- Only local registration/login was available

## 🛠️ Troubleshooting

### Issue: Google OAuth button not working
**Possible Causes:**
- Google Cloud Console configuration issues
- Redirect URI mismatch
- Application not restarted after credential changes

**Solutions:**
1. **Check Google Cloud Console Settings:**
   - Go to [Google Cloud Console](https://console.cloud.google.com/)
   - Navigate to APIs & Services > Credentials
   - Verify your OAuth 2.0 client configuration
   - Ensure authorized redirect URIs include:
     - `https://localhost:5001/signin-google`
     - `http://localhost:5000/signin-google`

2. **Verify Application Restart:**
   - Stop the application
   - Restart using `dotnet run`
   - Environment variables are loaded on startup

3. **Check Browser Console:**
   - Open browser developer tools (F12)
   - Look for any JavaScript errors
   - Check network tab for failed requests

### Issue: "Redirect URI mismatch" error
**Solution:**
Update your Google OAuth settings to include the correct redirect URIs:
- Development: `https://localhost:5001/signin-google`
- Production: `https://yourdomain.com/signin-google`

## 🎯 Next Steps

1. **Test the Authentication Flow**
   - Try logging in with Google
   - Verify user data is properly stored
   - Test logout functionality

2. **Add Sample Data**
   - Create some income transactions
   - Add expense transactions
   - Test the dashboard charts and summaries

3. **Explore All Features**
   - Dashboard overview
   - Transaction CRUD operations
   - Monthly reports
   - CSV export functionality

## 🔐 Security Notes

- Your `.env` file contains real secrets and is git-ignored
- Never share your Client Secret publicly
- The application automatically detects real vs placeholder credentials
- All user data is isolated per authenticated user

## 📞 Support

If you encounter any issues:
1. Check the console output for error messages
2. Verify your Google Cloud Console configuration
3. Ensure the application has been restarted after adding credentials
4. Test with a clean browser session (incognito mode)

---

**Status**: ✅ Ready for testing with Google OAuth enabled!
