using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class LibraryBook : BaseModelUpdatedAt
    {        
        public long IdLibrary { get; set; }        
        public ICollection<Image> Images { get; set; }
        public long IdBook { get; set; }
        public double Price { get; set; }
        public virtual Book Book { get; set; }
        public virtual Library Library { get; set; }
        public virtual ICollection<LibraryBookType> LibraryBookTypes { get; set; }
        public virtual ICollection<ContactBook> ContactBooks { get; set; }
    }
}