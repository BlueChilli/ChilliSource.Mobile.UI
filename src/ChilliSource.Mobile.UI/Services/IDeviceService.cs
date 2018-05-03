#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Contract for getting device and application related information. 
    /// </summary>
    public interface IDeviceService
    {

#if __ANDROID__

        void Init(Android.App.Activity mainActivity);
#endif

        #region App Info

        /// <summary>
        /// Returns the current app's version number.
        /// </summary>
        string GetAppVersion();

        /// <summary>
        /// Returns the current app's build number.
        /// </summary>
        string GetAppBuild();


        /// <summary>
        /// Gets a value indicating whether the current app is shown to the user.
        /// </summary>
        /// <value><c>true</c> if the app is in foreground; otherwise, <c>false</c>.</value>
        bool IsInForeground();

        /// <summary>
        /// Returns the unique identifier of the current app installation.
        /// </summary>
        /// <remarks>
        /// This identifier changes if the user uninstalls and reinstalls the app, and is therefore
        /// not the equivalent of a unique device id, but the closest that is available from iOS.
        /// </remarks>
        string GetUniqueId();

        #endregion

        #region OS Info

        /// <summary>
        /// Returns the system name and version separated by a space.
        /// </summary>
        string GetPlatform();

        /// <summary>
        /// Returns the name of the operating system, e.g. "iPhone OS".
        /// </summary>
        string GetSystemName();

        /// <summary>
        /// Returns the version of the operating system, e.g. "6.1.3".
        /// </summary>
        string GetSystemVersion();

        /// <summary>
        /// Returns the time zone of the device's location, e.g. "Europe/Paris".
        /// </summary>
        string GetTimeZone();

        #endregion

        #region Device Info

        /// <summary>
        /// Determines if the app is running on a physical device or the simulator.
        /// </summary>
        /// <value><c>true</c> if running on a physical device; otherwise, <c>false</c>.</value>
        bool IsPhysicalDevice { get; }

        /// <summary>
        /// Returns the personalized device name, e.g. "John's iPhone 7".
        /// </summary>
        string GetDeviceName();

        /// <summary>
        /// Returns the device's model, e.g. "iPhone".
        /// </summary>
        string GetDeviceModel();

        /// <summary>
        /// Returns the string representing the hardware version of the device.
        /// E.g. "iPhone3,3" representing an iPhone 4 device.
        /// </summary>
        string GetDeviceVersion();
       
        /// <summary>
        /// Returns the amount of free space on the device in bytes.
        /// </summary>
        /// <returns>
        /// A <see cref="OperationResult"/> instance representing the amount of free space.
        /// </returns>
        OperationResult<ulong> GetFreeSpace();

        #endregion

        #region UI Info

        /// <summary>
        /// Returns the device type as <see cref="UserInterfaceIdiom"/>
        /// </summary>        
        UserInterfaceIdiom GetUserInterfaceIdiom();

        /// <summary>
        /// Returns the width and height of the device screen in points.
        /// On Android make sure to call the Init method of this service in OnCreate of your MainActivity.
        /// </summary>
        /// <returns>A <see cref="Size"/> value that represents the screen size.</returns>
        Size GetScreenSize();

        /// <summary>
        /// Returns any device-specific padding around the edges of the screen to demarcate safe areas,
        /// e.g. for top and bottom padding for the iPhone X.         
        /// </summary>
        /// <returns></returns>
        Thickness GetSafeAreaInsets(bool includeStatusBar = false);

        /// <summary>
        /// Returns the screen height divided by the screen width
        /// </summary>
        /// <returns></returns>
        float GetScreenHeightToWidthRatio();

        #endregion

        #region Actions

        /// <summary>
        /// Prompts the user to authenticate through a biometric sensor, e.g. TouchID on an iPhone.
        /// </summary>
        /// <returns>A <see cref="OperationResult"/> instance representing the outcome of the operation.</returns>
        /// <param name="promptText">Prompt text.</param>
        Task<OperationResult> PerformBiometricAuthentication(string promptText);

        /// <summary>
        /// Marks the beginning of a new long-running background task.
        /// </summary>
        /// <remarks>
        /// Allows the app to continue running for a period of time to complete critical tasks.
        /// </remarks>
        /// <returns>A <see cref="int"/> value which is used as a unique id needed to end the task.</returns>
        int BeginBackgroundTask();

        /// <summary>
        /// Marks the end of a specific long-running background task, identified by <paramref name="taskId"/>.
        /// </summary>
        /// <remarks>
        /// You must call this method to end a task that was started with <c>BeginBackgroundTask</c> method.
        /// </remarks>
        /// <param name="taskId">Task identifier.</param>
        void EndBackgroundTask(int taskId);

        /// <summary>
        /// Forces the OS to change the UI orientation to the new <paramref name="orientation"/> provided.
        /// </summary>
        /// <param name="orientation">Orientation.</param>
        void ForceOrientation(Orientation orientation);

        /// <summary>
        /// Determines if an external app can be launched.
        /// </summary>
        /// <value><c>true</c> if app can be launched; otherwise, <c>false</c>.</value>
        /// <param name="url">App or Web url.</param>
        [Obsolete("use Device.OpenUri instead")]
        bool CanOpenExternalApp(string url);

        /// <summary>
        /// Launches an external app, specified by the provided url, e.g. "comgooglemaps://?center=37.422185,-122.083898&amp;zoom=10".
        /// </summary>
        /// <returns>A <see cref="OperationResult"/> instance representing the outcome of the operation.</returns>
        /// <param name="url">App or Web url.</param>
        [Obsolete("use Device.OpenUri instead")]
        OperationResult OpenExternalApp(string url);

        /// <summary>
        /// Launches the native phone app and dials the specified <paramref name="number"/>.
        /// </summary>
        /// <returns><c>true</c>, if phone app was successfuly launched, <c>false</c> otherwise.</returns>
        /// <param name="number">Number.</param>
        bool DialPhoneNumber(string number);

        /// <summary>
        /// Opens the settings page for the app
        /// </summary>
        /// <returns><c>true</c>, if app settings was successfuly launched, <c>false</c> otherwise.</returns>
        bool OpenSettings();

        #endregion
    }
}

