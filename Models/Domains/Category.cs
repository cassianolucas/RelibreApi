using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }        
        public virtual ICollection<CategoryBook> CategoryBooks { get; set; }
    }
}