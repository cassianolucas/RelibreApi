using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Contact : BaseModelUpdatedAt
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}