using System;
using Newtonsoft.Json;

namespace RelibreApi.Models
{
    public class BaseModelUpdatedAt : BaseModel
    {
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        public bool IsActive()
        {
            return Active;
        }
    }
}