using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Constants;
using TestApp.Services;

namespace TestApp.Managers
{
    public class VendorManager
    {
        public async Task<IRestResult<string>> AddVendor(string firstName, string lastName, string email, string password, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            var param = new { FirstName = firstName, LastName = lastName, Email = email, Password = password };
            return await RestAPI.PostAsync<string>(APIConstants.AddVendorURL, param, cancellationTokenSource);
        }
    }
}
