using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using XamarinMaps.Models;
using XamarinMaps.Services;
using static XamarinMaps.Models.PlacesLocations;

namespace XamarinMaps.ViewModels
{
    class MainPageViewModel
    {
        public MainPageViewModel()
        {
        }

        private ObservableCollection<AddressInfo> _addresses;
        public ObservableCollection<AddressInfo> Addresses
        {
            get => _addresses ?? (_addresses = new ObservableCollection<AddressInfo>());
            set
            {
                if (_addresses != value)
                {
                    _addresses = value;
                    //OnPropertyChanged();
                }
            }
        }

        private string _addressText;
        public string AddressText
        {
            get => _addressText;
            set
            {
                if (_addressText != value)
                {
                    _addressText = value;
                    //OnPropertyChanged();
                }
            }
        }

        internal async Task<System.Collections.Generic.List<Position>> LoadRoute(string startLot, string startLong, string endLot, string endLong)
        {
            var googleDirection = await ApiServices.ServiceClientInstance.GetDirections(startLot, startLong, endLot, endLong);
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                return positions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Add your payment method inside the Google Maps console.", "Ok");
                return null;

            }

        }
    }
}
