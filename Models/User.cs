using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class User : Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Profile Profile { get; set; }        
        public long IdPerson { get; set; }
    }
}