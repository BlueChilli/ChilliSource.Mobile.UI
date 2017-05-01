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

namespace ChilliSource.Mobile.UI.Core
{
	public interface IDeviceService
	{
		#region App Info

		/// <summary>
		/// Returns the current app's version number
		/// </summary>
		/// <returns>The app version.</returns>
		string GetAppVersion();

		/// <summary>
		/// Returns the current app's build number
		/// </summary>
		/// <returns>The app build.</returns>
		string GetAppBuild();


		/// <summary>
		/// Gets a value indicating whether the current app is shown to the user
		/// </summary>
		/// <value><c>true</c> if is in foreground; otherwise, <c>false</c>.</value>
		bool IsInForeground();

		/// <summary>
		/// Returns the unique identifier of the current app installation. 
		/// Note: this identifier changes if the user uninstalls and reinstalls the app, and is therefore
		/// not the equivalent of a unique device id, but the closest that is available from iOS
		/// </summary>
		/// <returns>The unique identifier.</returns>
		string GetUniqueId();

		#endregion

		#region OS Info

		/// <summary>
		/// Returns the system name and version separated by a space
		/// </summary>
		/// <returns>The platform.</returns>
		string GetPlatform();

		/// <summary>
		/// Returns the name of the operating system, e.g. "iPhone OS"
		/// </summary>
		/// <returns>The system name.</returns>
		string GetSystemName();

		/// <summary>
		/// Returns the version of the operating system, e.g. "6.1.3"
		/// </summary>
		/// <returns>The system version.</returns>
		string GetSystemVersion();

		/// <summary>
		/// Returns the time zone name of the device's location, e.g. "Europe/Paris"
		/// </summary>
		/// <returns>The time zone.</returns>
		string GetTimeZone();

		#endregion

		#region Device Info

		/// <summary>
		/// Determines if the app is running on a physical device or the simulator
		/// </summary>
		/// <value><c>true</c> if is physical device; otherwise, <c>false</c>.</value>
		bool IsPhysicalDevice { get; }

		/// <summary>
		/// Returns the personalized device name, e.g. "John's iPhone 7"
		/// </summary>
		/// <returns>The device name.</returns>
		string GetDeviceName();

		/// <summary>
		/// Returns the device's model, e.g. "iPhone"
		/// </summary>
		/// <returns>The device model.</returns>
		string GetDeviceModel();

		/// <summary>
		/// Returns the string representing the hardware version of the device.
		/// E.g. "iPhone3,3" representing an iPhone 4 device.
		/// </summary>
		/// <returns>The device version.</returns>
		string GetDeviceVersion();

		/// <summary>
		/// Returns the width and height of the device screen in points
		/// </summary>
		/// <returns>The screen size.</returns>
		Size GetScreenSize();

		/// <summary>
		/// Returns the amount of free space on the device in bytes
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance representing the outcome of the operation
		/// and holding the free space value</returns>
		OperationResult<ulong> GetFreeSpace();

		#endregion

		#region Actions

		/// <summary>
		/// Prompts the user to authenticate through a biometric sensor, e.g. TouchID on an iPhone
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance representing the outcome of the operation</returns>
		/// <param name="promptText">Prompt text.</param>
		Task<OperationResult> PerformBiometricAuthentication(string promptText);

		/// <summary>
		/// Marks the beginning of a new long-running background task. Allows the app
		/// to continue running for a period of time to complete critical tasks
		/// </summary>
		/// <returns>A unique id for the background task, needed to end the task.</returns>
		int BeginBackgroundTask();

		/// <summary>
		/// Marks the end of a specific long-running background task, identified by <paramref name="taskId"/>.
		/// You must call this method to end a task that was started with <see cref="BeginBackgroundTask"/>
		/// </summary>
		/// <param name="taskId">Task identifier.</param>
		void EndBackgroundTask(int taskId);


		/// <summary>
		/// Forces the OS to change the UI orientation to the new <paramref name="orientation"/> provided
		/// </summary>
		/// <param name="orientation">Orientation.</param>
		void ForceOrientation(Orientation orientation);


		/// <summary>
		/// Launches an external app, specified by the provided url, e.g. "comgooglemaps://?center=37.422185,-122.083898&zoom=10"
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance representing the outcome of the operation</returns>
		/// <param name="url">App or Web url.</param>
		OperationResult OpenExternalApp(string url);

		/// <summary>
		/// Launches the native phone app and dials the specified <paramref name="number"/>
		/// </summary>
		/// <returns><c>true</c>, if phone app was successfuly launched, <c>false</c> otherwise.</returns>
		/// <param name="number">Number.</param>
		bool DialPhoneNumber(string number);

		#endregion
	}
}

