using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class LibraryBookTypeMap : IEntityTypeConfiguration<LibraryBookType>
    {
        public void Configure(EntityTypeBuilder<LibraryBookType> o)
        {
            o.ToTable("library_book_type");

            o.HasKey(x => new { x.IdLibraryBook, x.IdType});

            o.Property(x => x.IdLibraryBook)
                .HasColumnName("id_library_book")
                .HasColumnType("bigint")
                .IsRequired();

            o.HasOne(x => x.LibraryBook)
                .WithMany(x => x.LibraryBookTypes)
                .HasForeignKey(x => x.IdLibraryBook)
                .HasConstraintName("fk_library_book_library_book_id_library_book")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.IdType)
                .HasColumnName("id_type")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Type)
                .WithMany(x => x.LibraryBookTypes)
                .HasForeignKey(x => x.IdType)
                .HasConstraintName("fk_library_book_library_book_id_type")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}