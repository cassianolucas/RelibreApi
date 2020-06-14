using System;
using System.Collections;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Report
    {
        public long Id { get; set; }
        public string Reason { get; set; }
        public bool State { get; set; }
        public long IdPerson { get; set; }
        public long IdPersonReport { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}