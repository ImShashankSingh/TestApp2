using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using TestApp.Droid.Activities;
using TestApp.Droid.Helper;
using TestApp.Managers;

namespace TestApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText emailEditText;
        EditText passwordEditText;
        Button loginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
        }

        protected override void OnResume()
        {
            base.OnResume();
            loginButton.Click += LoginButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            loginButton.Click -= LoginButton_Click;
        }
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            APIManager aPIManager = new APIManager();
            var response = await aPIManager.Login(emailEditText.Text, passwordEditText.Text);

            if (response.IsSuccess)
            {
                SharedPreferences.Instance.JwtToken = response.Data.Token;
                SharedPreferences.Instance.EmailId = response.Data.Token;
                TestApp.Helper.AppSecurity.Token = response.Data.Token;
                StartActivity(typeof(VendorRegistrationActivity));
                Finish();
            }
        }
    }
}

