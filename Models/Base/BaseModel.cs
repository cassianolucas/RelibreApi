using System;

namespace RelibreApi.Models
{
    public abstract class BaseModel : BaseModelSimple
    {                        
        public DateTime CreatedAt { get; set; }
    }
}