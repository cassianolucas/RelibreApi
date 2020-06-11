using System;

namespace RelibreApi.Models
{
    public class Phone
    {
        public long Id { get; set; }
        public long IdPerson { get; set; }
        public string Number { get; set; }
        public bool Master { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Person Person { get; set; }  
    }
}