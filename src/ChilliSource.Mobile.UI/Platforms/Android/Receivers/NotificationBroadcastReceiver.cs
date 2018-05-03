#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Content;
using Android.App;

namespace ChilliSource.Mobile.UI
{
	[BroadcastReceiver]
	public class NotificationBroadcastReceiver : BroadcastReceiver
	{
		public const string NotificationIdTag = "NotificationId";

		public const string NotificationTag = "Notification";

		public override void OnReceive(Context context, Intent intent)
		{
			var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

			var notification = (Notification)intent.GetParcelableExtra(NotificationTag);
			notificationManager.Notify(intent.GetIntExtra(NotificationIdTag, 0), notification);
		}
	}
}

