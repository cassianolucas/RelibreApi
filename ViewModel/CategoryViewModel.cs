using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class CategoryViewModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}