using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingYourLocation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackingYourLocation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationManagerPage : ContentPage
    {
        public NotificationManagerPage()
        {
            InitializeComponent();

            BindingContext = new NotificationManagerViewModel();
        }
    }
}