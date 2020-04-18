using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApp.Droid.Helper
{
    public static class Constants
    {
        public static string ListenConnectionString = "Endpoint=sb://demoappshashank.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=yzMpEcvONXIn6CkMk8X1fgaNY51rEgKa+oPcwF8ZaDQ=";
        public static string NotificationHubName = "DemoApp";
    }
}