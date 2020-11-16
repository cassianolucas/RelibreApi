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

        [JsonProperty(PropertyName = "full_address")]
        public string FullAddress { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "complement")]
        public string Complement { get; set; }
    }
}