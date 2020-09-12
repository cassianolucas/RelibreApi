namespace RelibreApi.Models
{
    public class AuthorBook
    {
        public long IdBook { get; set; }
        public virtual Book Book { get; set; }
        public long IdAuthor { get; set; }
        public virtual Author Author { get; set; }
    }
}