# Google OAuth Redirect URI Fix

## 🚨 Current Issue
- **Error**: "Access blocked: This app's request is invalid"
- **Code**: "Error 400: redirect_uri_mismatch"
- **Cause**: The redirect URI in Google Cloud Console doesn't match your application

## 🔧 Fix Steps

### 1. Go to Google Cloud Console
1. Open [Google Cloud Console](https://console.cloud.google.com/)
2. Select your project: "Budget Tracker (සල්ලි නැතෝ)"
3. Go to **APIs & Services** → **Credentials**
4. Click on your OAuth 2.0 Client ID

### 2. Update Authorized Redirect URIs
Add these URIs to your OAuth client configuration:

**For Development (HTTP):**
```
http://localhost:5000/signin-google
http://localhost:5001/signin-google
```

**For Production (HTTPS):**
```
https://localhost:5001/signin-google
https://yourdomain.com/signin-google
```

### 3. Save the Changes
- Click **Save** in the Google Cloud Console
- Wait 5-10 minutes for changes to propagate

### 4. Test the Application
1. Restart your Budget Tracker application
2. Go to http://localhost:5000
3. Click **Login** 
4. Try Google OAuth login again

## 📋 Current Application Configuration

**Your Client ID**: `756237020890-u0hb358unpap6c3dgk25vgdqg8p9mbob.apps.googleusercontent.com`

**Expected Redirect URI**: `http://localhost:5000/signin-google`

## ✅ Success Indicators
- No more "redirect_uri_mismatch" error
- Redirected to Google login page
- Successfully authenticated and redirected back to your app
