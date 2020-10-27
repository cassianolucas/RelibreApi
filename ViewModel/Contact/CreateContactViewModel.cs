using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class CreateContactViewModel
    {
        [JsonProperty(PropertyName = "id_book")]
        public long IdLibraryBook { get; set; }
    }
}