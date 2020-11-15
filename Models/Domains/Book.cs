using System;
using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Book : BaseModelSimple
    {                
        public string CodeIntegration { get; set; }
        public string Isbn13 { get; set; }
        public string Title { get; set; }
        public string MaturityRating { get; set; }
        public string AverageRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<CategoryBook> CategoryBooks { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}