using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class CreateContactPublicViewModel
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "id_book")]
        public long IdBook { get; set; }
    }
}