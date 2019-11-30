using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingYourLocation.Services
{
    public interface INotificationManager
    {
        event EventHandler<NotificationEventArgs> NotificationReceived;

        void Initialize();

        int ScheduleNotification(string title, string message);

        void ReceiveNotification(string title, string message);
    }
}
