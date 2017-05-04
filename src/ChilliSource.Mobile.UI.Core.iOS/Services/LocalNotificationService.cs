#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Foundation;
using UIKit;
using ChilliSource.Mobile.UI.Core;
using ChilliSource.Mobile.Core;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotificationService))]

namespace ChilliSource.Mobile.UI.Core
{
	public class LocalNotificationService : ILocalNotificationService
	{
		public LocalNotificationService()
		{
		}

		public void RegisterForNotifications(bool showAlert, bool showBadge, bool playSound)
		{
			UIUserNotificationType notificationType = UIUserNotificationType.None;
			if (showAlert)
			{
				notificationType |= UIUserNotificationType.Alert;
			}

			if (showBadge)
			{
				notificationType |= UIUserNotificationType.Badge;
			}

			if (playSound)
			{
				notificationType |= UIUserNotificationType.Sound;
			}

			var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(notificationType, null);

			UIApplication.SharedApplication.RegisterUserNotificationSettings(notificationSettings);
		}

		public void ScheduleNotification(int id, string title, string body, DateTime notifyTime, string tag)
		{
			var notification = new UILocalNotification();

			NSMutableDictionary dict = new NSMutableDictionary();

			dict.Add(new NSString("id"), new NSNumber(id));
			dict.Add(new NSString("tag"), new NSString(tag));

			notification.UserInfo = dict;
			notification.FireDate = notifyTime.ToNSDate();

			notification.AlertAction = null;
			notification.AlertBody = title;
			notification.RepeatInterval = 0;
			notification.SoundName = UILocalNotification.DefaultSoundName;

			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
		}

		public void CancelNotification(int id, string tag)
		{
			foreach (var notification in UIApplication.SharedApplication.ScheduledLocalNotifications)
			{
				var tuple = GetUserInfoIdAndTag(notification);

				if (tuple.Item1 == id && tag == tuple.Item2)
				{
					UIApplication.SharedApplication.CancelLocalNotification(notification);
					break;
				}
			}
		}

		public void CancelAllNotifications()
		{
			UIApplication.SharedApplication.CancelAllLocalNotifications();
		}

		public void ListAllScheduledNotifications()
		{
			if (UIApplication.SharedApplication.ScheduledLocalNotifications != null)
			{
				Console.WriteLine("--Scheduled Notifications--");
				foreach (var notification in UIApplication.SharedApplication.ScheduledLocalNotifications)
				{
					var tuple = GetUserInfoIdAndTag(notification);
					Console.WriteLine(tuple.Item1 + "\t\t" + tuple.Item2 + "\t" + notification.AlertTitle);
				}
			}
		}

		#region Helper Methods

		private Tuple<int, string> GetUserInfoIdAndTag(UILocalNotification notification)
		{
			int storedId = 0;
			string storedTag = string.Empty;

			if (notification.UserInfo != null)
			{
				if (notification.UserInfo["id"] != null)
				{
					storedId = (notification.UserInfo["id"] as NSNumber).Int32Value;
				}

				if (notification.UserInfo["tag"] != null)
				{
					storedTag = notification.UserInfo["tag"] as NSString;
				}
			}

			return new Tuple<int, string>(storedId, storedTag);
		}

		#endregion
	}
}


