using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Helper
{
    internal class AppSecurity
    {
        public static string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1ZGE0YmQ1ZS02MDQyLTQ5YTMtYjQ2ZC03NmZlMDY1Y2FkMGQiLCJmaXJzdE5hbWUiOiJzdHJpbmciLCJsYXN0TmFtZSI6InN0cmluZyIsImVtYWlsIjoiZW1haWxAZW1haWwuY29tIiwiZXhwIjoxNTg3MDI3Mzc5LCJpc3MiOiJJc3N1ZXJOYW1lIiwiYXVkIjoiSXNzdWVyTmFtZSJ9.jkiCmWuICsurxJeX4onA5PDIOCOV-UnQqz7LkdSEv7o";//{ get; private set; }
        public static bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Token);
            }
        }
    }
}
