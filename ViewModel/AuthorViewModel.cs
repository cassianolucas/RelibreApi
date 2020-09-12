using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class AuthorViewModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}