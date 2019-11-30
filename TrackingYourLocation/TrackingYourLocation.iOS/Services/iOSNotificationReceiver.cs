using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ObjCRuntime;
using TrackingYourLocation.Services;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace TrackingYourLocation.iOS.Services
{
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(
            UNUserNotificationCenter center, 
            UNNotification notification, 
            Action<UNNotificationPresentationOptions> completionHandler)
        {
            base.WillPresentNotification(center, notification, completionHandler);

            var notificationManager = DependencyService.Get<INotificationManager>();
            if (notificationManager != null)
            {
                notificationManager.ReceiveNotification(notification.Request.Content.Title, notification.Request.Content.Body);
            }

            // alerts are always shown for demonstration but this can be set to "None"
            // to avoid showing alerts if the app is in the foreground
            completionHandler(UNNotificationPresentationOptions.Alert);

        }
    }
}