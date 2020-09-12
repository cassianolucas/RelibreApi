using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class ImageViewModel
    {
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
    }
}