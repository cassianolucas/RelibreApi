using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string IdIntegration { get; set; }
        public string Isbn13 { get; set; }
        public string Title { get; set; }
        public string MaturityRating { get; set; }
        public string AverageRating { get; set; }        
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}