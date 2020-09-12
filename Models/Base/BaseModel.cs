using System;

namespace RelibreApi.Models
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive()
        {
            return Active;
        }
    }
}