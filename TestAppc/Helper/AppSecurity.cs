using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Helper
{
    public class AppSecurity
    {
        public static string Token { get; set; }
        public static bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Token);
            }
        }
    }
}
