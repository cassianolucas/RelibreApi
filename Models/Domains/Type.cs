using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Type
    {
        // emprestimo, troca etc...
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}