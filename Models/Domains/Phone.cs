using System;

namespace RelibreApi.Models
{
    public class Phone : BaseModelUpdatedAt
    {        
        public long IdPerson { get; set; }
        public string Number { get; set; }
        public bool Master { get; set; }
        public virtual Person Person { get; set; }  
    }
}