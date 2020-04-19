using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Constants;
using TestApp.Services;

namespace TestApp.Managers
{
    public class APIManager
    {
        public async Task<IRestResult<Login>> Login(string email, string password)
        {
            var param = new { email, password };
            return await RestAPI.PostAsync<Login>("/api/User/Login", param);
        }

        public async Task<IRestResult<string>> AddVendor(string firstName, string lastName, string email, string password, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            var param = new { FirstName = firstName, LastName = lastName, Email = email, Password = password };
            return await RestAPI.PostAsync<string>(APIConstants.AddVendorURL, param, cancellationTokenSource);
        }

        public async Task<IRestResult<string>> RegisterForNotification(object data, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            return await RestAPI.PostAsync<string>(APIConstants.RegisterForNotificationURL, data, cancellationTokenSource);
        }

        public async Task<IRestResult<string>> SendNotification(string recieverTag, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            var param = new { tagOfReciver = recieverTag };
            return await RestAPI.QueryPostAsync<string>("/api/User/NotificationToVendor?recieverTag=" + recieverTag, cancellationTokenSource);
        }
    }

    public class Login
    {
        public string Token { get; set; }
    }
}
