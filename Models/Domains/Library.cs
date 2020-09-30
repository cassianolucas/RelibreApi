using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Library : BaseModelUpdatedAt
    {        
        public long IdPerson { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}