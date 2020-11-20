using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class RateViewModel
    {
        [JsonProperty(PropertyName = "note")]
        public int Note { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }   

        [JsonProperty(PropertyName = "id_contact")]
        public long IdContact { get; set; }

        [JsonProperty(PropertyName = "id_book")]
        public long IdBook { get; set; }
        
        [JsonProperty(PropertyName = "param")]
        public string Param { get; set; }
    }
}