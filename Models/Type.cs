using System;

namespace RelibreApi.Models
{
    public class Type
    {
        // emprestimo, troca etc...
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}