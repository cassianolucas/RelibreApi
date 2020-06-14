using System;

namespace RelibreApi.Models
{
    public class NotificationPerson
    {
        public long Id { get; set; }
        public bool State { get; set; }                
        public long IdNotification { get; set; }
        public long IdPerson { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual Person Person { get; set; }

    }
}