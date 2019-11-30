using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TrackingYourLocation.Services;
using UIKit;
using TrackingYourLocation.iOS.Services;
using Xamarin.Forms;
using UserNotifications;

[assembly: Dependency(typeof(iOSNotificationManager))]
namespace TrackingYourLocation.iOS.Services
{
    public class iOSNotificationManager : INotificationManager
    {
        int messageId = -1;

        bool hasNotificationsPermission;

        public event EventHandler<NotificationEventArgs> NotificationReceived;

        public void Initialize()
        {
            // request the permission to use local notifications
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
            {
                hasNotificationsPermission = approved;
            });
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message
            };

            NotificationReceived?.Invoke(null, args);
        }

        public int ScheduleNotification(string title, string message)
        {
            // EARLY OUT: app doesn't have permissions
            if (!hasNotificationsPermission)
            {
                return -1;
            }
            messageId = Xamarin.Essentials.Preferences.Get(PreferenceKeys.MESSAGE_ID, 1);
            messageId++;
            Xamarin.Essentials.Preferences.Set(PreferenceKeys.MESSAGE_ID, messageId);

            var content = new UNMutableNotificationContent()
            {
                Title = title,
                Subtitle = "",
                Body = message,
                Badge = 1
            };

            // Local notifications can be time or location based
            // Create a time-based trigger, interval is in seconds and must be greater than 0
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);

            var request = UNNotificationRequest.FromIdentifier(messageId.ToString(), content, trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                {
                    throw new Exception($"Failed to schedule notification: {err}");
                }
            });

            return messageId;
        }
    }
}