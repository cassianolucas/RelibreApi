using System;
using System.Collections;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Complaint
    {
        public long Id { get; set; }
        public string Reason { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}