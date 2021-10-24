using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinMaps.ViewModels;

namespace XamarinMaps
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel mainPageViewModel;
        Position myPosition;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = mainPageViewModel = new MainPageViewModel();
            RequestPositionAndMoveOnTheMap();
        }

        public async void RequestPositionAndMoveOnTheMap()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            myPosition = new Position(position.Latitude, position.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(myPosition, Distance.FromMiles(1)));
        }

        public async void OnLocationEnterTextChanged(System.Object sender, System.EventArgs e)
        {
            /*if (!string.IsNullOrWhiteSpace(ViewModel.AddressText))
            {
                await ViewModel.GetPlacesPredictionsAsync();
            }*/
        }
        public async void LocationNameEnter_Focused(System.Object sender, System.EventArgs e)
        {
            /*if (!string.IsNullOrWhiteSpace(mainPageViewModel.AddressText))
            {
                await mainPageViewModel.GetPlacesPredictions();
            }*/
        }

        public async void PickStartLocation(System.Object sender, System.EventArgs e)
        {
            var pathcontent = await mainPageViewModel.LoadRoute(myPosition.Latitude.ToString(), myPosition.Longitude.ToString(), $"51.677950", $"39.301170");

            map.MapElements.Clear();
            //map.Polylines.Clear();

            var polyline = new Polyline();
            polyline.StrokeColor = Color.Black;
            polyline.StrokeWidth = 3;

            foreach (var p in pathcontent)
            {
                polyline.Geopath.Add(p);
            }
            map.MapElements.Add(polyline);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Geopath[0].Latitude, polyline.Geopath[0].Longitude), Distance.FromMiles(0.50f)));

            var pin = new Pin
            {
                Type = PinType.SearchResult,
                Position = new Position(polyline.Geopath.First().Latitude, polyline.Geopath.First().Longitude),
                Label = "Pin",
                Address = "Pin",
                //Tag = "CirclePoint",
                //Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_circle_point.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_circle_point.png", WidthRequest = 25, HeightRequest = 25 })

            };
            map.Pins.Add(pin);

            var positionIndex = 1;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (pathcontent.Count > positionIndex)
                {
                    UpdatePostions(pathcontent[positionIndex]);
                    positionIndex++;
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public async void PickEndLocation(System.Object sender, System.EventArgs e)
        {

        }
            async void UpdatePostions(Position position)
        {
            if (map.Pins.Count == 1 && map.MapElements != null && map.MapElements?.Count > 1)
                return;

            var cPin = map.Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Position(position.Latitude, position.Longitude);
                //cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 25, HeightRequest = 25 });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMeters(200)));
                //var previousPosition = map.MapElements?.FirstOrDefault()?.Geopath?.FirstOrDefault();
                //map.MapElements?.FirstOrDefault()?.Geopath?.Remove(previousPosition.Value);
            }
            else
            {
                //map.MapElements?.FirstOrDefault()?.Geopath?.Clear();
            }
        }
        public void OnMapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
        }

        private void LocationNameEnter_Focused(object sender, FocusEventArgs e)
        {

        }
    }
}
