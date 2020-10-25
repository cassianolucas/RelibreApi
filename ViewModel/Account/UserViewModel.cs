using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class UserViewModel
    {

        [JsonProperty(PropertyName = "name")]        
        public string Name { get; set; }

        [JsonProperty(PropertyName = "last_name")]        
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "login")]        
        public string Login { get; set; }

        [JsonProperty(PropertyName = "document")]        
        public string Document { get; set; }

        [JsonProperty(PropertyName = "phones")]
        public ICollection<PhoneViewModel> Phones { get; set; }

        [JsonProperty(PropertyName = "addresses")]
        public ICollection<AddressViewModel> Addresses { get; set; }
    }
}