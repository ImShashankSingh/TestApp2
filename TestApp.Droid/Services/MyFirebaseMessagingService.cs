using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using TestApp.Droid.Activities;
using WindowsAzure.Messaging;
using TestApp.Droid.Helper;

namespace TestApp.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        
        public override void OnMessageReceived(RemoteMessage message)
        {
            if (message.GetNotification() != null)
            {
                //These is how most messages will be received
                SendNotification(message.GetNotification().Body);
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal
                SendNotification(message.Data.Values.First());

            }
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(VendorRegistrationActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, VendorRegistrationActivity.CHANNEL_ID);

            notificationBuilder.SetContentTitle("FCM Message")
                        .SetSmallIcon(Resource.Drawable.ic_launcher)
                        .SetContentText(messageBody)
                        .SetAutoCancel(true)
                        .SetShowWhen(false)
                        .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0, notificationBuilder.Build());
        }

        //public override void OnNewToken(string token)
        //{
        //    SendRegistrationToServer(token);
        //}

        //void SendRegistrationToServer(string token)
        //{
        //    // Register with Notification Hubs
            
        //    Log.Debug(TAG, $"Successful registration of ID {regID}");
        //}
    }
}