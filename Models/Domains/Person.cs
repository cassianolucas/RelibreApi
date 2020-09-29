using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Person : BaseModel
    {                
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string PersonType { get; set; }
        public DateTime Birthday { get; set; }
        public virtual Library Library { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<NotificationPerson> NotificationPeople { get; set; }

    }
}