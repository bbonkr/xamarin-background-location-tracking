using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrackingYourLocation.Services;
using TrackingYourLocation.Views;
using System.Threading.Tasks;

namespace TrackingYourLocation
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            // Handle when your app starts
            RegisterNotificationManager();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            // Handle when your app resumes
            RegisterNotificationManager();
        }

        private void RegisterNotificationManager()
        {
            if (notificationManager == null)
            {
                notificationManager = DependencyService.Get<INotificationManager>();
                notificationManager.NotificationReceived += (sender, eventArgs) => {
                    var eventData = eventArgs;

                    MessagingCenter.Instance.Send(this, MessagingCenterMessages.NOTIFICATION_RECEIVED, eventArgs);
                };
            }
        }

        private INotificationManager notificationManager;
    }
}
