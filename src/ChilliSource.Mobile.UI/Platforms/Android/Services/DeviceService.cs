#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using Android.Content;
using Android.App;
using System.Collections.Generic;
using Android.OS;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.UI;
using System.Threading.Tasks;
using Android.Util;

[assembly: Dependency(typeof(DeviceService))]
namespace ChilliSource.Mobile.UI
{
    public class DeviceService : IDeviceService
    {
        Activity _mainActivity;

        public void Init(Activity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        #region App Info

        private Context Context => Android.App.Application.Context;

        public string GetAppVersion()
        {
            Context context = Context;
            return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
        }


        public string GetAppBuild()
        {
            return "";
        }

        public bool IsInForeground()
        {
            Context context = Context;
            ActivityManager activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            IList<ActivityManager.RunningAppProcessInfo> appProcesses = activityManager.RunningAppProcesses;
            if (appProcesses == null)
            {
                return false;
            }
            string packageName = context.PackageName;
            foreach (ActivityManager.RunningAppProcessInfo appProcess in appProcesses)
            {
                if (appProcess.Importance == Importance.Background && appProcess.ProcessName == packageName)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetUniqueId()
        {
            return Android.OS.Build.Serial;
        }

        #endregion

        #region OS Info

        public string GetPlatform()
        {
            return GetSystemName() + " " + GetSystemVersion();
        }

        public string GetSystemName()
        {
            return "Android";
        }

        public string GetSystemVersion()
        {
            return Build.VERSION.Release;
        }


        public string GetTimeZone()
        {
            return Java.Util.TimeZone.Default.ID;
        }

        #endregion

        #region Device Info

        public bool IsPhysicalDevice
        {
            get
            {
                string fingerprint = Build.Fingerprint;

                bool isEmulator = false;

                if (fingerprint != null)
                {
                    isEmulator = fingerprint.Contains("vbox") || fingerprint.Contains("generic") || fingerprint.Contains("vsemu");
                }

                return !isEmulator;
            }
        }

        public string GetDeviceName()
        {            
            return Android.Provider.Settings.Global.GetString(Context.ContentResolver, "bluetooth_name");
        }

        public string GetDeviceModel()
        {
            return Build.Model;
        }

        public string GetDeviceVersion()
        {
            return Build.Manufacturer;
        }
      
        public OperationResult<ulong> GetFreeSpace()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UI Info

        public UserInterfaceIdiom GetUserInterfaceIdiom()
        {
            throw new NotImplementedException();
        }

        public Xamarin.Forms.Size GetScreenSize()
        {            
            if (_mainActivity?.WindowManager == null)
            {
                return new Xamarin.Forms.Size();
            }

            var metrics = new DisplayMetrics();            
            _mainActivity.WindowManager.DefaultDisplay.GetRealMetrics(metrics);
            
            var widthInDp = metrics.WidthPixels / metrics.Density;
            var heightInDp = metrics.HeightPixels / metrics.Density;

            return new Xamarin.Forms.Size(widthInDp, heightInDp);
        }
        
        public Thickness GetSafeAreaInsets(bool includeStatusBar = false)
        {            
            return new Thickness() { Top = includeStatusBar ? GetStatusBarHeight() : 0 };
        }

        int GetStatusBarHeight()
        {
            const int DefaultStatusBarHeight = 24;

            int resourceId = Context.Resources.GetIdentifier("status_bar_height", "dimen", "android");

            if (resourceId <= 0)
            {
                return DefaultStatusBarHeight;
            }            
            var pixelSize = Context.Resources.GetDimensionPixelSize(resourceId);
            
            if (_mainActivity == null)
            {
                return DefaultStatusBarHeight;
            }

            var metrics = new DisplayMetrics();
            _mainActivity.WindowManager.DefaultDisplay.GetRealMetrics(metrics);

            return (int)(pixelSize / metrics.Density);
        }

        public float GetScreenHeightToWidthRatio()
        {
            var size = GetScreenSize();
            return (float)(size.Height / size.Width);
        }

        #endregion

        #region Actions

        public Task<OperationResult> PerformBiometricAuthentication(string promptText)
        {
            throw new NotImplementedException();
        }

        public int BeginBackgroundTask()
        {
            throw new NotImplementedException();
        }

        public void EndBackgroundTask(int taskId)
        {
            throw new NotImplementedException();
        }

        public void ForceOrientation(Orientation orientation)
        {
            throw new NotImplementedException();
        }

        public bool CanOpenExternalApp(string url)
        {
            return true;
        }

        public OperationResult OpenExternalApp(string url)
        {
            var uri = Android.Net.Uri.Parse(url);
            var intent = new Intent(Intent.ActionView, uri);
            Context.StartActivity(intent);
            return OperationResult.AsSuccess();
        }

        public bool DialPhoneNumber(string number)
        {
            var intent = new Intent(Intent.ActionCall, Android.Net.Uri.Parse("tel:" + number));
            Context.StartActivity(intent);
            return true;
        }
       
        public bool OpenSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings, 
                Android.Net.Uri.Parse("package:" + Android.App.Application.Context.PackageName));
            Context.StartActivity(intent);
            return true;
        }

        #endregion       
    }
}

