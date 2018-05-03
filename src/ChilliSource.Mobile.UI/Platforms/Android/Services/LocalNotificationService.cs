#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.App;
using Xamarin.Forms;
using Android.Content;

using Android.OS;
using ChilliSource.Mobile.UI;
using ChilliSource.Mobile.Core;
using Xamarin.Forms.PlatformConfiguration;

[assembly: Dependency(typeof(LocalNotificationService))]

namespace ChilliSource.Mobile.UI
{
    public class LocalNotificationService : ILocalNotificationService
    {
        private Context Context => Android.App.Application.Context;

        public LocalNotificationService()
        {

        }

        public void RegisterForNotifications(bool showAlert, bool showBadge, bool playSound)
        {
            //Do nothing here since implementation not relevant for Android
        }

        public void ScheduleNotification(int id, string title, string body, DateTime? notifyTime, string tag)
        {
            //TODO: add tag to notification

            Intent notificationIntent = new Intent(Context, typeof(NotificationBroadcastReceiver));
            notificationIntent.PutExtra(NotificationBroadcastReceiver.NotificationIdTag, id);
            notificationIntent.PutExtra(NotificationBroadcastReceiver.NotificationTag, BuildNotification(id, title, body));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this.Context, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

            AlarmManager alarmManager = (AlarmManager)this.Context.GetSystemService(Context.AlarmService);
            var time = notifyTime.HasValue ? GetNotifyTimeInMilliseconds(notifyTime.Value) : 0;
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + time, pendingIntent);
        }

        public void CancelNotification(int id, string tag)
        {
            var notificationManager = this.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Cancel(tag, id);
        }

        public void ListAllScheduledNotifications()
        {

        }

        public void CancelAllNotifications()
        {
            var notificationManager = this.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.CancelAll();
        }

        private Notification BuildNotification(int id, string title, string body)
        {
            Intent intent = new Intent("com.chillisource.notification.manager");
            intent.PutExtra("notificationId", id);
            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(this.Context)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetDefaults(NotificationDefaults.All)
                .SetContentIntent(PendingIntent.GetBroadcast(this.Context, 0, intent, PendingIntentFlags.OneShot));

            // Build the notification:
            return builder.Build();
        }

        private long GetNotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcAlarmTimeInMillis = (notifyTime.ToUniversalTime() - DateTime.UtcNow).TotalMilliseconds;
            return (long)utcAlarmTimeInMillis;
        }
    }
}

