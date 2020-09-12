using System;

namespace RelibreApi.Models
{
    public class NotificationPerson : BaseModel
    {
        public long IdNotification { get; set; }
        public long IdPerson { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual Person Person { get; set; }

    }
}