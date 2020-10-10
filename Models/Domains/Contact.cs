using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class Contact : BaseModelUpdatedAt
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }        
        public virtual ICollection<ContactBook> ContactBooksOwner { get; set; }
        public virtual ICollection<ContactBook> ContactBooksRequest { get; set; }
    }
}