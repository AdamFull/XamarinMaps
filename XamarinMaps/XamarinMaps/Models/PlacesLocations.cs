using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMaps.Models
{
    class PlacesLocations
    {
        public class AddressInfo
        {

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string ZipCode { get; set; }

            public double Longitude { get; set; }

            public double Latitude { get; set; }
        }

        public class PlacesMatchedSubstring
        {

            [Newtonsoft.Json.JsonProperty("length")]
            public int Length { get; set; }

            [Newtonsoft.Json.JsonProperty("offset")]
            public int Offset { get; set; }
        }

        public class PlacesTerm
        {

            [Newtonsoft.Json.JsonProperty("offset")]
            public int Offset { get; set; }

            [Newtonsoft.Json.JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Prediction
        {

            [Newtonsoft.Json.JsonProperty("id")]
            public string Id { get; set; }

            [Newtonsoft.Json.JsonProperty("description")]
            public string Description { get; set; }

            [Newtonsoft.Json.JsonProperty("matched_substrings")]
            public List<PlacesMatchedSubstring> MatchedSubstrings { get; set; }

            [Newtonsoft.Json.JsonProperty("place_id")]
            public string PlaceId { get; set; }

            [Newtonsoft.Json.JsonProperty("reference")]
            public string Reference { get; set; }

            [Newtonsoft.Json.JsonProperty("terms")]
            public List<PlacesTerm> Terms { get; set; }

            [Newtonsoft.Json.JsonProperty("types")]
            public List<string> Types { get; set; }
        }

        public class PlacesLocationPredictions
        {

            [Newtonsoft.Json.JsonProperty("predictions")]
            public List<Prediction> Predictions { get; set; }

            [Newtonsoft.Json.JsonProperty("status")]
            public string Status { get; set; }
        }
    }
}
