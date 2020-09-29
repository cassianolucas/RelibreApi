using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class LibraryBook : BaseModel
    {        
        public long IdLibrary { get; set; }
        public long IdContact { get; set; }
        public ICollection<Image> Images { get; set; }
        public long IdBook { get; set; }
        public string Reating { get; set; }
        public virtual Book Book { get; set; }
        public virtual Library Library { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ICollection<LibraryBookType> LibraryBookTypes { get; set; }
    }
}