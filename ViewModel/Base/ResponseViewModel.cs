using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class ResponseViewModel
    {
        [JsonProperty(PropertyName = "result")]
        public object Result { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}