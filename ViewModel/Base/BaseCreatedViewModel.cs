using System;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class BaseCreatedViewModel
    {
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}