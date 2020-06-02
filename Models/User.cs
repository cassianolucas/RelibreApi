namespace RelibreApi.Models
{
    public class User
    {
        public string Login { get; set; }   
        public string Password { get; set; }
        public Person Person { get; set; }
        public Profile Profile { get; set; }
    }
}