using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Profile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<AccessProfile> AccessProfiles { get; set; }
    }
}