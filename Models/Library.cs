using System;

namespace RelibreApi.Models
{
    public class Library
    {
        public long Id { get; set; }
        public long IdPerson { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public Person Person { get; set; }
    }
}