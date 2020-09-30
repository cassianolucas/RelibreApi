using System;

namespace RelibreApi.Models
{
    public class Address : BaseModelUpdatedAt
    {        
        public long IdPerson { get; set; }
        public string NickName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FullAddress { get; set; }
        public bool Master { get; set; }
        public virtual Person Person { get; set; }
    }
}