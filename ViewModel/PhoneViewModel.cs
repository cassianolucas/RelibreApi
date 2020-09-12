using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class PhoneViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        
        [JsonProperty(PropertyName = "id_person")]
        public long IdPerson { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "master")]
        public bool Master { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
    }
}