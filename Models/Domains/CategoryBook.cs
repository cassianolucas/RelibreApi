namespace RelibreApi.Models
{
    public class CategoryBook
    {
        public long IdBook { get; set; }
        public virtual Book Book { get; set; }
        public long IdCategory { get; set; }
        public virtual Category Category { get; set; }
    }
}