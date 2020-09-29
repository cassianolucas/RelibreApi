namespace RelibreApi.Models
{
    public class LibraryBookType
    {
        public long IdLibraryBook { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
        public long IdType { get; set; }
        public virtual Type Type { get; set; }
    }
}