using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMaps.Services;

namespace XamarinMaps
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.Black };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
