using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Extensions;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using TestApp.Managers;
using WindowsAzure.Messaging;

namespace TestApp.Droid.Activities
{
    [Activity(Label = "VendorRegistration", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class VendorRegistrationActivity : AppCompatActivity
    {
        private TextView tokenTextView;
        private TextView registrationIdTextView;
        private EditText tagEditText;
        private Button registerNotificationButton;
        private EditText recieverTagEditText;
        private Button submitButton;
        APIManager vendorManager;
        public const string TAG = "VendorRegistrationActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";

        NotificationHub hub;
        public const string ListenConnectionString = "Endpoint=sb://demoappshashank.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=yzMpEcvONXIn6CkMk8X1fgaNY51rEgKa+oPcwF8ZaDQ=";
        public const string NotificationHubName = "DemoApp";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VendorRegistrationLayout);


            AppCenter.Start("b186992c-ac43-41c0-844f-672451c1ace6",
                               typeof(Analytics), typeof(Crashes));


            tokenTextView = FindViewById<TextView>(Resource.Id.tokenTextView);
            registrationIdTextView = FindViewById<TextView>(Resource.Id.registrationIdTextView);
            tagEditText = FindViewById<EditText>(Resource.Id.tagEditText);
            registerNotificationButton = FindViewById<Button>(Resource.Id.registerForNotification);
            recieverTagEditText = FindViewById<EditText>(Resource.Id.recieverTagEditText);
            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            IsPlayServicesAvailable();
#if DEBUG
            Task.Run(() => 
            {
                FirebaseInstanceId.Instance.DeleteInstanceId();
                FirebaseInstanceId.Instance.GetInstanceId();
            });
#endif
            vendorManager = new APIManager();

            GetToken();

            CreateNotificationChannel();
        }

        private async Task GetToken()
        {
            var instanceIdResult = await FirebaseInstanceId.Instance.GetInstanceId().AsAsync<IInstanceIdResult>();
            tokenTextView.Text = instanceIdResult.Token;
        }

        protected override void OnResume()
        {
            base.OnResume();
            registerNotificationButton.Click += RegisterNotificationButton_Click;
            submitButton.Click += SubmitButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            registerNotificationButton.Click -= RegisterNotificationButton_Click;
            submitButton.Click -= SubmitButton_Click;
        }

        private async void RegisterNotificationButton_Click(object sender, EventArgs e)
        {
            //var tags = new List<string>() { tagEditText.Text };
            var param = new { platform = "gcm", handle = tokenTextView.Text, userType = "Vendor", userEmail = tagEditText.Text };
            //var regID = hub.Register(tokenTextView.Text, tags.ToArray()).RegistrationId;
            var regId = await vendorManager.RegisterForNotification(param);
            registrationIdTextView.Text = regId.Data;
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            //VendorManager vendorManager = new VendorManager();
            //var t = await vendorManager.AddVendor(tokenTextView.Text, tagEditText.Text, emailEditText.Text, passwordEditText.Text);
            var response = await vendorManager.SendNotification(recieverTagEditText.Text);
            recieverTagEditText.Text = "Notification sent";
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