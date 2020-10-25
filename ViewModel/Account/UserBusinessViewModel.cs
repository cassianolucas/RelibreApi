using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class UserBusinessViewModel : UserRegisterViewModel
    {
        [JsonProperty(PropertyName = "document")]
        public string Document { get; set; }
        
    }
}