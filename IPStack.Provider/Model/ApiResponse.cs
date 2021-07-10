using Newtonsoft.Json;

namespace IPStack.Adapter.Model
{
    public partial class ApiResponse
    {
        [JsonProperty("ip")]
        public string IP { get; set; }

        [JsonProperty("city")]
        public string City { get ; set; }

        [JsonProperty("country_name")]
        public string Country { get ; set ; }

        [JsonProperty("continent_name")]
        public string Continent { get ; set ; }

        [JsonProperty("latitude")]
        public string Latitude { get ; set ; }

        [JsonProperty("longitude")]
        public string Longitude { get ; set ; }
    }
}
