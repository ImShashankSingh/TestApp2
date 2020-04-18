using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using TestApp.Managers;

namespace TestApp.Droid.Activities
{
    [Activity(Label = "VendorRegistration", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class VendorRegistrationActivity : AppCompatActivity
    {
        private EditText firstNameEditText;
        private EditText lastNameEditText;
        private EditText emailEditText;
        private EditText passwordEditText;
        private Button submitButton;

        public const string TAG = "VendorRegistrationActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VendorRegistrationLayout);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            firstNameEditText = FindViewById<EditText>(Resource.Id.firstNameEditText);
            lastNameEditText = FindViewById<EditText>(Resource.Id.lastNameEditText);
            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            IsPlayServicesAvailable();
#if DEBUG
            Task.Run(() => 
            {
                FirebaseInstanceId.Instance.DeleteInstanceId();
                FirebaseInstanceId.Instance.GetInstanceId();
            });
#endif
            CreateNotificationChannel();
        }

        protected override void OnResume()
        {
            base.OnResume();
            submitButton.Click += SubmitButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            submitButton.Click -= SubmitButton_Click;
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            VendorManager vendorManager = new VendorManager();
            var t = await vendorManager.AddVendor(firstNameEditText.Text, lastNameEditText.Text, emailEditText.Text, passwordEditText.Text);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Debug(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Debug(TAG, "This device is not supported");
                    Finish();
                }
                return false;
            }

            Log.Debug(TAG, "Google Play Services is available.");
            return true;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = CHANNEL_ID;
            var channelDescription = string.Empty;
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };
            
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}