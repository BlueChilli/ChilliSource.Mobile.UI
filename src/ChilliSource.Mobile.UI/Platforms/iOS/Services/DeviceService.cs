#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Foundation;
using Xamarin.Forms;
using UIKit;
using LocalAuthentication;
using System.Runtime.InteropServices;
using ObjCRuntime;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.UI;
using System.Threading.Tasks;
using System.Net;

[assembly: Dependency(typeof(DeviceService))]

namespace ChilliSource.Mobile.UI
{
    public class DeviceService : IDeviceService
    {        
        private const string _hardwareProperty = "hw.machine";

        [DllImport(Constants.SystemLibrary)]
        internal static extern int GetDeviceProperty([MarshalAs(UnmanagedType.LPStr)] string property, // name of the property
                                                IntPtr output, // output
                                                IntPtr oldLen, // IntPtr.Zero
                                                IntPtr newp, // IntPtr.Zero
                                                uint newlen // 0
                                                );

        #region App Info

        public string GetAppVersion()
        {
            return NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
        }

        public string GetAppBuild()
        {
            return NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
        }

        public bool IsInForeground()
        {
            return UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active;
        }

        public string GetUniqueId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }

        #endregion

        #region OS Info

        public string GetPlatform()
        {
            return GetSystemName() + " " + GetSystemVersion();
        }

        public string GetSystemName()
        {
            return UIDevice.CurrentDevice.SystemName;
        }

        public string GetSystemVersion()
        {
            return UIDevice.CurrentDevice.SystemVersion;
        }

        public string GetTimeZone()
        {
            return NSTimeZone.LocalTimeZone.Name;
        }

        #endregion

        #region Device Info

        public bool IsPhysicalDevice
        {
            get
            {
                return ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.DEVICE;
            }
        }

        public string GetDeviceName()
        {
            return UIDevice.CurrentDevice.Name;
        }

        public string GetDeviceModel()
        {
            return UIDevice.CurrentDevice.Model;
        }

        public string GetDeviceVersion()
        {
            try
            {
                // get the length of the string that will be returned
                var pLen = Marshal.AllocHGlobal(sizeof(int));
                GetDeviceProperty(_hardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);

                var length = Marshal.ReadInt32(pLen);

                // check to see if we got a length
                if (length == 0)
                {
                    Marshal.FreeHGlobal(pLen);
                    return "Unknown";
                }

                // get the hardware string
                var pStr = Marshal.AllocHGlobal(length);
                GetDeviceProperty(_hardwareProperty, pStr, pLen, IntPtr.Zero, 0);

                // convert the native string into a C# string
                var hardwareStr = Marshal.PtrToStringAnsi(pStr);

                // cleanup
                Marshal.FreeHGlobal(pLen);
                Marshal.FreeHGlobal(pStr);

                return hardwareStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeviceService.GetDeviceVersion: " + ex.Message);
            }

            return "Unknown";
        }
       
        public OperationResult<ulong> GetFreeSpace()
        {
            var attributes = NSFileManager.DefaultManager?.GetFileSystemAttributes(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            if (attributes != null)
            {
                return OperationResult<ulong>.AsSuccess(attributes.FreeSize);
            }
            else
            {
                return OperationResult<ulong>.AsFailure("Could not retrieve personal folder attributes");
            }
        }

        #endregion

        #region UI Info

        public UserInterfaceIdiom GetUserInterfaceIdiom()
        {
            switch (UIDevice.CurrentDevice.UserInterfaceIdiom)
            {
                case UIUserInterfaceIdiom.CarPlay: return UserInterfaceIdiom.Car;
                case UIUserInterfaceIdiom.Pad: return UserInterfaceIdiom.Tablet;
                case UIUserInterfaceIdiom.Phone: return UserInterfaceIdiom.Phone;
                case UIUserInterfaceIdiom.TV: return UserInterfaceIdiom.TV;
                default: return UserInterfaceIdiom.Unspecified;
            }
        }

        public Size GetScreenSize()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            return new Size(bounds.Width, bounds.Height);
        }

        public Thickness GetSafeAreaInsets(bool includeStatusBar = false)
        {
            var result = new Thickness();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                var windows = UIApplication.SharedApplication?.Windows;

                if (windows != null && windows.Length > 0)
                {
                    var insets = windows[0].SafeAreaInsets;

                    result.Left = insets.Left;
                    result.Right = insets.Right;
                    result.Top = insets.Top;
                    result.Bottom = insets.Bottom;
                }
            }

            if (result.Top > 0) //if iPhone X
            {
                //iPhone X insets already include the status bar
                return result;
            }

            if (includeStatusBar)
            {
                result.Top += 20;
            }

            return result;
        }

        public float GetScreenHeightToWidthRatio()
        {
            return (float)(UIScreen.MainScreen.Bounds.Height / UIScreen.MainScreen.Bounds.Width);
        }


        #endregion

        #region Actions

        public Task<OperationResult> PerformBiometricAuthentication(string promptText)
        {
            var tcs = new TaskCompletionSource<OperationResult>();

            var context = new LAContext();
            NSError authError;
            var message = new NSString(promptText);

            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out authError))
            {
                var replyHandler = new LAContextReplyHandler((success, error) =>
                {
                    if (success)
                    {
                        tcs.SetResult(OperationResult.AsSuccess());
                    }
                    else
                    {
                        tcs.SetResult(OperationResult.AsFailure((error as NSError).LocalizedDescription));
                    }

                });

                // This starts the authentication dialog. 
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, message, replyHandler);

            }
            return tcs.Task;
        }

        public int BeginBackgroundTask()
        {
            int taskId = 0;

            Action expirationHandler = () =>
            {
                Console.WriteLine("Exhausted time");
                if (taskId != 0)
                {
                    UIApplication.SharedApplication.EndBackgroundTask(taskId);
                }
            };

            //expirationHandler only called if background time allowed exceeded
            taskId = Convert.ToInt32(UIApplication.SharedApplication.BeginBackgroundTask(expirationHandler));

            return taskId;
        }

        public void EndBackgroundTask(int taskId)
        {
            if (taskId != 0)
            {
                UIApplication.SharedApplication.EndBackgroundTask(taskId);
            }
        }

        public bool CanOpenExternalApp(string url)
        {
            var nativeUrl = new NSUrl(url);
            return UIApplication.SharedApplication.CanOpenUrl(nativeUrl);
        }

        public OperationResult OpenExternalApp(string url)
        {
            var nativeUrl = new NSUrl(url);

            if (UIApplication.SharedApplication.CanOpenUrl(nativeUrl))
            {
                bool success = UIApplication.SharedApplication.OpenUrl(nativeUrl);
                if (success)
                {
                    return OperationResult.AsSuccess();
                }
                else
                {
                    return OperationResult.AsFailure("Failed to launch app with url");
                }
            }
            else
            {
                return OperationResult.AsFailure("App url cannot be opened by iOS");
            }
        }

        public bool DialPhoneNumber(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + WebUtility.UrlEncode(number)));
        }

        public void ForceOrientation(Orientation orientation)
        {
            UIInterfaceOrientation uiOrienation = UIInterfaceOrientation.Portrait;
            switch (orientation)
            {
                case Orientation.Portrait:
                    {
                        uiOrienation = UIInterfaceOrientation.Portrait;
                        break;
                    }
                case Orientation.LandscapeLeft:
                    {
                        uiOrienation = UIInterfaceOrientation.LandscapeLeft;
                        break;
                    }
                case Orientation.LandscapeRight:
                    {
                        uiOrienation = UIInterfaceOrientation.LandscapeRight;
                        break;
                    }
                case Orientation.PortraitUpsideDown:
                    {
                        uiOrienation = UIInterfaceOrientation.PortraitUpsideDown;
                        break;
                    }
            }

            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)uiOrienation), new NSString("orientation"));
        }

        public bool OpenSettings()
        {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
        }

        #endregion
    }
}

