#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using ChilliSource.Mobile.Core;
using System.Collections.Generic;
using ChilliSource.Mobile.UI;

[assembly: Dependency(typeof(AlertService))]

namespace ChilliSource.Mobile.UI
{
	public class AlertService : IAlertService
	{
		public ExtendedFont TitleFont { get; set; }

		public ExtendedFont MessageFont { get; set; }

		public Task DisplayAlert(string title, string message, string option = "Ok")
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			SetAttributedText(alert, title, message);

			alert.AddAction(UIAlertAction.Create(option, UIAlertActionStyle.Cancel, action => taskCompletionSource.SetResult(true)));

            NavigationHelper.GetActiveViewController().PresentViewController(alert, animated: true, completionHandler: null);

			return taskCompletionSource.Task;
		}

        public Task<bool> DisplayYesNoAlert(string title, string message, string affirmativeOption = "Yes", string negativeOption = "No", bool isAffirmativeDestructive = false)
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			SetAttributedText(alert, title, message);

			alert.AddAction(UIAlertAction.Create(negativeOption, UIAlertActionStyle.Cancel, action => taskCompletionSource.SetResult(false)));
			alert.AddAction(UIAlertAction.Create(affirmativeOption, isAffirmativeDestructive ? UIAlertActionStyle.Destructive : UIAlertActionStyle.Default, action => taskCompletionSource.SetResult(true)));

            NavigationHelper.GetActiveViewController().PresentViewController(alert, animated: true, completionHandler: null);

			return taskCompletionSource.Task;
		}

		public Task<string> DisplayTextInputAlert(string title, string message)
		{
			var taskCompletionSource = new TaskCompletionSource<string>();

			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			SetAttributedText(alert, title, message);

			alert.AddTextField(textField =>
		   {
		   });

			alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alertAction => taskCompletionSource.SetCanceled()));
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, alertAction => taskCompletionSource.SetResult(alert.TextFields[0].Text)));

            NavigationHelper.GetActiveViewController().PresentViewController(alert, animated: true, completionHandler: null);

			return taskCompletionSource.Task;
		}

		public Task<bool> DisplaySettingsAlert(string title, string message, string cancel = "Cancel")
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			SetAttributedText(alert, title, message);

			UIAlertAction cancelAction = UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, action => taskCompletionSource.SetResult(false));
			alert.AddAction(cancelAction);

			// Provide quick access to Settings.
			UIAlertAction settingsAction = UIAlertAction.Create("Settings", UIAlertActionStyle.Default, action =>
			{
				UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
				taskCompletionSource.SetResult(true);
			});

			alert.AddAction(settingsAction);
            NavigationHelper.GetActiveViewController().PresentViewController(alert, true, null);

			return taskCompletionSource.Task;
		}

        public Task<string> DisplayActionSheet(string title, string cancelOption, string destructiveOption, params string[] options)
		{
			var taskCompletionSource = new TaskCompletionSource<string>();

			var alert = UIAlertController.Create(title, null, UIAlertControllerStyle.ActionSheet);

			SetAttributedText(alert, title, null);

            if (!string.IsNullOrEmpty(cancelOption))
			{
				alert.AddAction(UIAlertAction.Create(cancelOption, UIAlertActionStyle.Cancel, action => taskCompletionSource.SetResult(string.Empty)));
			}

			foreach (string option in options)
			{
				alert.AddAction(UIAlertAction.Create(option, UIAlertActionStyle.Default, action => taskCompletionSource.SetResult(option)));
			}

            if (!string.IsNullOrEmpty(destructiveOption))
			{

				alert.AddAction(UIAlertAction.Create(destructiveOption, UIAlertActionStyle.Destructive, action => taskCompletionSource.SetResult(destructiveOption)));
			}

            NavigationHelper.GetActiveViewController().PresentViewController(alert, animated: true, completionHandler: null);

			return taskCompletionSource.Task;
		}

        public OperationResult DisplaySharingSheet(string text, string description, string filePath = "")
        {
            var items = new List<NSObject>() { new NSString(text) };

            if (!string.IsNullOrEmpty(description))
            {
                items.Add(new NSString(description));
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                var url = NSUrl.FromFilename(filePath);
                items.Add(url);
            }

            var controller = new UIActivityViewController(items.ToArray(), null);
            var parentController = NavigationHelper.GetActiveViewController();
            parentController.PresentViewController(controller, true, null);
            return OperationResult.AsSuccess();
        }

        #region Helper Methods

        private void SetAttributedText(UIAlertController alert, string title, string message)
		{
			if (TitleFont != null && !string.IsNullOrEmpty(title))
			{
				var attributedTitle = TitleFont.BuildAttributedString(title);
				alert.SetValueForKey(attributedTitle, new NSString("attributedTitle"));
			}
			if (MessageFont != null && !string.IsNullOrEmpty(message))
			{
				var attributedMessage = MessageFont.BuildAttributedString(message);
				alert.SetValueForKey(attributedMessage, new NSString("attributedMessage"));
			}
		}

		#endregion
	}
}

