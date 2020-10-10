using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Notification : BaseModel
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<NotificationPerson> NotificationPeople { get; set; }
    }
}