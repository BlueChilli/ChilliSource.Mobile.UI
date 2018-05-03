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
using Android.Content;

[assembly: Dependency(typeof(AlertService))]

namespace ChilliSource.Mobile.UI
{
	public class AlertService : IAlertService
	{
	    private Context Context => Android.App.Application.Context;

		public ExtendedFont TitleFont { get; set; }

		public ExtendedFont MessageFont { get; set; }


		public Task DisplayAlert(string title, string message, string option)
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);

			builder.SetPositiveButton(option, (senderAlert, args) =>
		   {
			   taskCompletionSource.SetResult(true);
		   });

			ApplyCustomFont(builder, title, message);

			return taskCompletionSource.Task;
		}

        public Task<bool> DisplayYesNoAlert(string title, string message, string affirmativeOption, string negativeOption, bool isAffirmativeDestructive = false)
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);

			builder.SetPositiveButton(affirmativeOption, (senderAlert, args) =>
		   {
			   taskCompletionSource.SetResult(true);
		   });

			builder.SetNegativeButton(negativeOption, (senderAlert, args) =>
		   {
			   taskCompletionSource.SetResult(false);
		   });

			ApplyCustomFont(builder, title, message);

			return taskCompletionSource.Task;
		}

		public Task<string> DisplayTextInputAlert(string title, string message)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DisplaySettingsAlert(string title, string message, string cancel = "Cancel")
		{
			throw new NotImplementedException();
		}

		public Task<string> DisplayActionSheet(string title, string cancelOption, string destructiveOption, params string[] options)
		{
            throw new NotImplementedException();
		}

		public OperationResult DisplaySharingSheet(string text, string description, string filePath = "")
		{
            throw new NotImplementedException();
        }

		private void ApplyCustomFont(AlertDialog.Builder builder, string title, string message)
		{
			var titleView = new TextView(this.Context)
			{
				Text = title
			};
			TitleFont.ApplyTo(titleView);
			var frame = new FrameLayout(this.Context);
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

