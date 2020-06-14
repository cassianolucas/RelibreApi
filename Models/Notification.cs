using System;

namespace RelibreApi.Models
{
    public class Notification
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public DateTime CreatedAt { get; set; }
    }
}