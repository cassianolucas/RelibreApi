namespace RelibreApi.Models
{
    public class Images
    {
        public long Id { get; set; }
        public string Thumbnail { get; set; }
        public string Small { get; set; }
        public string Medium { get; set; }
        public string Large { get; set; }
        public string SmallThumbnail { get; set; }
        public string ExtraLarge { get; set; }
        public long IdBook { get; set; }
        public virtual Book Book { get; set; }
    }
}