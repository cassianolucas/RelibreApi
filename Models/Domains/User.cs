using System.Collections.Generic;

namespace RelibreApi.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long IdProfile { get; set; }
        public Profile Profile { get; set; }
        public long IdPerson { get; set; }
        public virtual Person Person { get; set; }

        public User()
        {
            this.Person = new Person();
        }

    }
}