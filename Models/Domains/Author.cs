using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Author : BaseModelUpdatedAt
    {        
        public string Name { get; set; }        
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}