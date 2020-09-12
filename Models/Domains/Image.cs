namespace RelibreApi.Models
{
    public class Image :  BaseModel
    {        
        public string Thumbnail { get; set; }
        public string Small { get; set; }
        public string Medium { get; set; }
        public string Large { get; set; }
        public string SmallThumbnail { get; set; }
        public string ExtraLarge { get; set; }
        public long IdLibraryBook { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
    }
}