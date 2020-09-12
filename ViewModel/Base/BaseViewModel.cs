using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public abstract class BaseViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public long id { get; set; }
        
    }
}