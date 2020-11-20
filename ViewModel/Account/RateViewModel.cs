using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class RateViewModel
    {
        [JsonProperty(PropertyName = "note")]
        public int Note { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }    
    }
}