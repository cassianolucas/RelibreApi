namespace RelibreApi.Models
{
    public class Image :  BaseModelSimple
    {        
        public string ImageLink { get; set; }
        public long IdLibraryBook { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
    }
}