using System;
using System.Collections;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Report : BaseModel
    {        
        public string Reason { get; set; }
        public long IdPerson { get; set; }
        public long IdPersonReport { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}