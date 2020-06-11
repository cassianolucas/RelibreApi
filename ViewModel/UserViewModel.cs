using System.Collections.Generic;

namespace RelibreApi.ViewModel
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Login { get; set; }   
        public string Password { get; set; }
        public ICollection<string> Phones { get; set; }
    }
}