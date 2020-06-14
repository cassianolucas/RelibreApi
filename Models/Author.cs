using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}