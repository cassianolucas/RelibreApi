using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Person
    {        
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string TypePerson { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Library Library { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<NotificationPerson> NotificationPeople { get; set; }

    }
}