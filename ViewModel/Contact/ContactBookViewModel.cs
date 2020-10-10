namespace RelibreApi.ViewModel
{
    public class ContactBookViewModel
    {
        public long IdContact { get; set; }
        public string FullName { get; set; }
        public bool Available { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}