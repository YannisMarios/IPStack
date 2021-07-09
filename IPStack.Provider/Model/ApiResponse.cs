using Newtonsoft.Json;

namespace IPStack.Adapter.Model
{
    public partial class ApiResponse
    {
        [JsonProperty("city")]
        public string City { get ; set; }

        [JsonProperty("country_name")]
        public string Country { get ; set ; }

        [JsonProperty("country_name")]
        public string Continent { get ; set ; }

        [JsonProperty("country_name")]
        public string Latitude { get ; set ; }

        [JsonProperty("country_name")]
        public string Longitude { get ; set ; }
    }
}
