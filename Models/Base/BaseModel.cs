using System;
using Newtonsoft.Json;

namespace RelibreApi.Models
{
    public abstract class BaseModel : BaseModelSimple
    {        
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        public bool IsActive()
        {
            return Active;
        }
    }
}