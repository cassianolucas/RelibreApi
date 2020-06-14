using System;

namespace RelibreApi.Models
{
    public class Address
    {
        public long Id { get; set; }
        public long IdPerson { get; set; }
        public string NickName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Master { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Person Person { get; set; }
    }
}