using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class CreateContactViewModel
    {
        [JsonProperty(PropertyName = "id_library_book")]        
        public long IdLibraryBook { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }
    }
}