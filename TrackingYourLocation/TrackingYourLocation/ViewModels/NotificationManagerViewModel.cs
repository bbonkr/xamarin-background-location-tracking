using kr.bbon.Xamarin.Forms;
using kr.bbon.Xamarin.Forms.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TrackingYourLocation.Models;
using TrackingYourLocation.Services;
using Xamarin.Forms;

namespace TrackingYourLocation.ViewModels
{
    public class NotificationManagerViewModel : ViewModelBase
    {
        public NotificationManagerViewModel() 
            : base()
        {
            MessagingCenter.Instance.Subscribe<App, NotificationEventArgs>(this, MessagingCenterMessages.NOTIFICATION_RECEIVED, (sender, eventArgs) => {
                Notifications.Add(new NotificationItem
                {
                    Title = eventArgs.Title,
                    Message = eventArgs.Message,
                });

                Notifications = Notifications;

            });
        }

        public ICommand SendNotificationCommand { get; set; }

        public string NotificationTitle
        {
            get => notificationTitle;
            set => SetProperty(ref notificationTitle, value);
        }

        public string NotificationMessage
        {
            get => notificationMessage;
            set => SetProperty(ref notificationMessage, value);
        }

        public ObservableCollection<NotificationItem> Notifications
        {
            get => notifications;
            set => SetProperty(ref notifications, value);
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            SendNotificationCommand = new Command(() => SendNotification());
        }

        private void SendNotification()
        {
            var notificationManager = DependencyService.Get<INotificationManager>();

            if (notificationManager != null)
            {
                var title = String.IsNullOrWhiteSpace(NotificationTitle) ?
                    "Hello!" :
                    NotificationTitle;

                var message = String.IsNullOrWhiteSpace(NotificationMessage) ?
                    "Hello world! - Local Notification" :
                    NotificationMessage;

                notificationManager.ScheduleNotification(title, message);
            }
        }

        private string notificationTitle;
        private string notificationMessage;
        private ObservableCollection<NotificationItem> notifications = new ObservableCollection<NotificationItem>();
    }
}
