using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Category : BaseModel
    {        
        public string Name { get; set; }
        public virtual ICollection<CategoryBook> CategoryBooks { get; set; }
    }
}