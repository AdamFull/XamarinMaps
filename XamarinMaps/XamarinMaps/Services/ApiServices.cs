using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XamarinMaps.Models;
using static XamarinMaps.Models.PlacesLocations;

namespace XamarinMaps.Services
{
    class ApiServices
    {
        private JsonSerializer _serializer = new JsonSerializer();

        private static ApiServices _ServiceClientInstance;

        public static ApiServices ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                    _ServiceClientInstance = new ApiServices();
                return _ServiceClientInstance;
            }
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

        private HttpClient client;
        public ApiServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
        }

        public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            var response = await client.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={KeyHolder.MyKeyHolder.GoogleMapsApiKey}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    googleDirection = await Task.Run(() =>
                       JsonConvert.DeserializeObject<GoogleDirection>(json)
                    ).ConfigureAwait(false);

                }
            }

            return googleDirection;
        }

        public async Task GetPlacesPredictions(string _addressText)
        {
            var region = RegionInfo.CurrentRegion.ToString();
            PlacesLocationPredictions placesLocations = new PlacesLocationPredictions();
            var response = await client.GetAsync($"api/place/autocomplete/json?key={KeyHolder.MyKeyHolder.GooglePlacesApiKey}&input={WebUtility.UrlEncode(_addressText)}&components=country:{region}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    placesLocations = await Task.Run(() => JsonConvert.DeserializeObject<PlacesLocationPredictions>(json)).ConfigureAwait(false);

                    if (placesLocations.Status == "OK")
                    {
                        Addresses.Clear();
                        if (placesLocations.Predictions.Count > 0)
                        {
                            foreach (Prediction prediction in placesLocations.Predictions)
                            {
                                Addresses.Add(new AddressInfo
                                {
                                    Address = prediction.Description
                                });
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(placesLocations.Status);
                    }
                }
            }
        }
    }
}
