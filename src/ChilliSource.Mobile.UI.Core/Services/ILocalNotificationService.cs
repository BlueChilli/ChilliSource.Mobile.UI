#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

namespace ChilliSource.Mobile.UI.Core
{
	public interface ILocalNotificationService
	{
		/// <summary>
		/// Tells the OS to start accepting local notifications based on the parameters provided
		/// </summary>
		/// <param name="showAlert">If set to <c>true</c> show alert.</param>
		/// <param name="showBadge">If set to <c>true</c> show badge.</param>
		/// <param name="playSound">If set to <c>true</c> play sound.</param>
		void RegisterForNotifications(bool showAlert, bool showBadge, bool playSound);

		/// <summary>
		/// Schedules a new local notification.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="title">Title.</param>
		/// <param name="body">Body.</param>
		/// <param name="notifyTime">Notify time.</param>
		/// <param name="tag">Tag.</param>
		void ScheduleNotification(int id, string title, string body, DateTime notifyTime, string tag);

		/// <summary>
		/// Cancels the local notification. Both the id and the tag are used to find
		/// the notification to cancel.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="tag">Tag.</param>
		void CancelNotification(int id, string tag);

		/// <summary>
		/// Cancels all local notifications.
		/// </summary>
		void CancelAllNotifications();

		/// <summary>
		/// Lists all scheduled notifications.
		/// </summary>
		void ListAllScheduledNotifications();

	}
}


