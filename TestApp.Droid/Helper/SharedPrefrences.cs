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
    public class SharedPreferences
    {
        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor sharedPreferencesEditor;


        public SharedPreferences()
        {
            sharedPreferences = Application.Context.GetSharedPreferences(Application.Context.PackageName, FileCreationMode.Private);
            sharedPreferencesEditor = sharedPreferences.Edit();
        }

        static SharedPreferences instance;

        public static SharedPreferences Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SharedPreferences();
                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        //public bool RememberMe
        //{
        //    get
        //    {
        //        return sharedPreferences.GetBoolean(AndroidConstants.RememberMeKey, false);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutBoolean(AndroidConstants.RememberMeKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}

        //public bool IsApplicationRunning
        //{
        //    get
        //    {
        //        return sharedPreferences.GetBoolean(AndroidConstants.IsApplicationRunningKey, false);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutBoolean(AndroidConstants.IsApplicationRunningKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}

        //public bool IsExecuted
        //{
        //    get
        //    {
        //        return sharedPreferences.GetBoolean(AndroidConstants.IsExecutedKey, false);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutBoolean(AndroidConstants.IsExecutedKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}

        //public string CompanyId
        //{
        //    get
        //    {
        //        return sharedPreferences.GetString(AndroidConstants.CompanyIdKey, string.Empty);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutString(AndroidConstants.CompanyIdKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}

        //public string Roles
        //{
        //    get
        //    {
        //        return sharedPreferences.GetString(AndroidConstants.RolesKey, string.Empty);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutString(AndroidConstants.RolesKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}


        //public string CompanyName
        //{
        //    get
        //    {
        //        return sharedPreferences.GetString(AndroidConstants.CompanyNameKey, string.Empty);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutString(AndroidConstants.CompanyNameKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}

        //public bool LoggedIn
        //{
        //    get
        //    {
        //        return sharedPreferences.GetBoolean(AndroidConstants.LoggedInKey, false);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutBoolean(AndroidConstants.LoggedInKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}


        //public string CreatorName
        //{
        //    get
        //    {
        //        return sharedPreferences.GetString(AndroidConstants.CreatorNameKey, string.Empty);
        //    }
        //    set
        //    {
        //        sharedPreferencesEditor.PutString(AndroidConstants.CreatorNameKey, value);
        //        sharedPreferencesEditor.Apply();
        //    }
        //}

        public string JwtToken
        {
            get
            {
                return sharedPreferences.GetString("JwtTokenKey", string.Empty);
            }
            set
            {
                sharedPreferencesEditor.PutString("JwtTokenKey", value);
                sharedPreferencesEditor.Apply();
            }
        }
        public string EmailId
        {
            get
            {
                return sharedPreferences.GetString("EmailIdKey", string.Empty);
            }
            set
            {
                sharedPreferencesEditor.PutString("EmailIdKey", value);
                sharedPreferencesEditor.Apply();
            }
        }

        //public void ResetSharedPreference()
        //{
        //    RememberMe = false;
        //    IsApplicationRunning = false;
        //    IsExecuted = false;
        //    CompanyId = string.Empty;
        //    CompanyName = string.Empty;
        //    LoggedIn = false;
        //    CreatorName = string.Empty;
        //    JwtToken = string.Empty;
        //    NameId = string.Empty;
        //}
    }
}