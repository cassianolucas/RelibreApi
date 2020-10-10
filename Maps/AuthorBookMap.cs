using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class AuthorBookMap : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> o)
        {
            o.ToTable("author_book");

            o.HasKey(x => new { x.IdBook, x.IdAuthor });

            o.Property(x => x.IdAuthor)
                .HasColumnName("id_author")
                .HasColumnType("bigint")
                .IsRequired();

            o.Property(x => x.IdBook)
                .HasColumnName("id_book")
                .HasColumnType("bigint")
                .IsRequired();

            o.HasOne(x => x.Book)
                .WithMany(x => x.AuthorBooks)
                .HasForeignKey(x => x.IdBook)
                .HasConstraintName("fk_author_book_book_id_book")
                .OnDelete(DeleteBehavior.Restrict);
            
            o.HasOne(x => x.Author)
                .WithMany(x => x.AuthorBooks)
                .HasForeignKey(x => x.IdAuthor)
                .HasConstraintName("fk_author_book_author_id_author")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}