using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class TypeViewModel : BaseViewModel
    {   
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}