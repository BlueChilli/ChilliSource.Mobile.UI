#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using ChilliSource.Mobile.UI;
using ChilliSource.Mobile.Core;

[assembly: Dependency(typeof(AlertService))]

namespace ChilliSource.Mobile.UI
{
	public class AlertService : IAlertService
	{
		public ExtendedFont TitleFont { get; set; }

		public ExtendedFont MessageFont { get; set; }


		public Task DisplayAlert(string title, string message, string option)
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			AlertDialog.Builder builder = new AlertDialog.Builder(Forms.Context);

			builder.SetPositiveButton(option, (senderAlert, args) =>
		   {
			   taskCompletionSource.SetResult(true);
		   });

			ApplyCustomFont(builder, title, message);

			return taskCompletionSource.Task;
		}

		public Task<bool> DisplayYesNoAlert(string title, string message, string positive, string negative, bool destructivePositive = false)
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			AlertDialog.Builder builder = new AlertDialog.Builder(Forms.Context);

			builder.SetPositiveButton(positive, (senderAlert, args) =>
		   {
			   taskCompletionSource.SetResult(true);
		   });

			builder.SetNegativeButton(negative, (senderAlert, args) =>
		   {
			   taskCompletionSource.SetResult(false);
		   });

			ApplyCustomFont(builder, title, message);

			return taskCompletionSource.Task;
		}

		public Task<string> DisplayTextInputAlert(string title, string message)
		{
			var taskCompletionSource = new TaskCompletionSource<string>();

			return taskCompletionSource.Task;
		}

		public Task<bool> DisplaySettingsAlert(string title, string message, string cancel = "Cancel")
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			return taskCompletionSource.Task;
		}

		public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] options)
		{
			var taskCompletionSource = new TaskCompletionSource<string>();

			return taskCompletionSource.Task;
		}

		public OperationResult DisplaySharingSheet(string text, string filePath = "")
		{
			return OperationResult.AsCancel();
		}

		private void ApplyCustomFont(AlertDialog.Builder builder, string title, string message)
		{
			var titleView = new TextView(Forms.Context)
			{
				Text = title
			};
			TitleFont.ApplyTo(titleView);
			var frame = new FrameLayout(Forms.Context);
			var layoutParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent);
			layoutParams.SetMargins(16, 16, 16, 16);
			frame.AddView(titleView, layoutParams);
			builder.SetCustomTitle(frame);
			builder.SetMessage(message);

			var dialog = builder.Show();

			var messageView = (TextView)dialog.FindViewById(Android.Resource.Id.Message);
			MessageFont.ApplyTo(messageView);
		}
	}
}

