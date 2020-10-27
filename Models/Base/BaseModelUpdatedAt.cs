using System;
using Newtonsoft.Json;

namespace RelibreApi.Models
{
    public class BaseModelUpdatedAt : BaseModel
    {        
        public bool Active { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive()
        {
            return Active;
        }
    }
}