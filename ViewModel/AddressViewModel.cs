using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class AddressViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "id_person")]
        public long IdPerson { get; set; }

        [JsonProperty(PropertyName = "nick_name")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }

        [JsonProperty(PropertyName = "master")]
        public bool Master { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
    }
}