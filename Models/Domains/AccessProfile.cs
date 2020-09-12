namespace RelibreApi.Models
{
    public class AccessProfile
    {
        public long Id { get; set; }
        public string Access { get; set; }
        public long IdProfile { get; set; }
        public virtual Profile Profile { get; set; }
    }
}