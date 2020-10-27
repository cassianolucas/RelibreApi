using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Type : BaseModelUpdatedAt
    {
        // emprestimo, troca etc...        
        public string Description { get; set; }
        public virtual ICollection<LibraryBookType> LibraryBookTypes { get; set; }
    }
}