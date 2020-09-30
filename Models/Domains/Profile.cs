using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Profile : BaseModelUpdatedAt
    {        
        public string Name { get; set; }
        public virtual ICollection<AccessProfile> AccessProfiles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}