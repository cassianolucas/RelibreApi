
namespace RelibreApi.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public long IdProfile { get; set; }
        public Profile Profile { get; set; }
        public long IdPerson { get; set; }
        public bool LoginVerified { get; set; }
        public virtual Person Person { get; set; }
        public int TotalCount { get; set; }
        public int TotalValue { get; set; }

        public User()
        {
            this.Person = new Person();
        }
        public bool IsVerified()
        {
            return LoginVerified;
        }

    }
}