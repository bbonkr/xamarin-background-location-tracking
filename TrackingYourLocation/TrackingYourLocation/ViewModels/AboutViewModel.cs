using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace TrackingYourLocation.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(async () =>
            {
                var uri = new Uri("https://xamarin.com/platform");
                // ! Deprecated
                //Device.OpenUri(uri);

                var ok = await Xamarin.Essentials.Launcher.CanOpenAsync(uri);
                if (ok)
                {
                    await Xamarin.Essentials.Launcher.TryOpenAsync(uri);
                }
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}