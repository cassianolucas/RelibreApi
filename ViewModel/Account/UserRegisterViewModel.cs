using System;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class UserRegisterViewModel 
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public DateTime BirthDate { get; set; }
    }
}