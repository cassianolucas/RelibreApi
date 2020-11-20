
using System;

namespace RelibreApi.Models
{    
    public class ContactBook
    {
        public long IdContactOwner { get; set; }
        public virtual Contact ContactOwner { get; set; }
        public long IdContactRequest { get; set; }
        public virtual Contact ContactRequest { get; set; }
        public long IdLibraryBook { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}