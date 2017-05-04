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
using ChilliSource.Mobile.UI.Core;
using System.Threading.Tasks;
using Android.Provider;

[assembly: Dependency(typeof(DeviceService))]

namespace ChilliSource.Mobile.UI.Core
{
	public class DeviceService : IDeviceService
	{
		#region App Info

		public string GetAppVersion()
		{
			Context context = Forms.Context;
			return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
		}


		public string GetAppBuild()
		{
			return "";
		}

		public bool IsInForeground()
		{
			Context context = Forms.Context;
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
			throw new NotImplementedException();
			//return Settings.System.GetString(GetContentResolver(), "device_name");
		}

		public string GetDeviceModel()
		{
			return Build.Model;
		}

		public string GetDeviceVersion()
		{
			return Build.Manufacturer;
		}

		public Size GetScreenSize()
		{
			throw new NotImplementedException();
		}

		public OperationResult<ulong> GetFreeSpace()
		{
			throw new NotImplementedException();
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

		public OperationResult OpenExternalApp(string url)
		{
			throw new NotImplementedException();
		}

		public bool DialPhoneNumber(string number)
		{
			Intent intent = new Intent(Intent.ActionCall, Android.Net.Uri.Parse("tel:" + number));
			Forms.Context.StartActivity(intent);
			return true;
		}

		#endregion

	}
}

