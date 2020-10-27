namespace RelibreApi.Models
{
    public class AccessProfile : BaseModelSimple
    {
        public string Access { get; set; }
        public long IdProfile { get; set; }
        public virtual Profile Profile { get; set; }
    }
}