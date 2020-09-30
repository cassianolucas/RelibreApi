using System;
using Newtonsoft.Json;

namespace RelibreApi.Models
{
    public abstract class BaseModel : BaseModelSimple
    {        
        
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }                
    }
}