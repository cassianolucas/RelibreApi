using System.Collections.Generic;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class UserBusinessViewModel : BaseViewModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "legal_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "document")]
        public string Document { get; set; }

        [JsonProperty(PropertyName = "web_site")]
        public string WebSite { get; set; }

        [JsonProperty(PropertyName = "url_image")]
        public string UrlImage { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "addresses")]
        public ICollection<AddressViewModel> Addresses { get; set; }

        [JsonProperty(PropertyName = "valid_plan")]
        public bool ValidPlan { get; set; }
    }
}