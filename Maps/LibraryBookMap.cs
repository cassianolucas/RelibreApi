using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class LibraryBookMap : IEntityTypeConfiguration<LibraryBook>
    {
        public void Configure(EntityTypeBuilder<LibraryBook> o)
        {
            o.ToTable("library_book");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.IdLibrary)
                .HasColumnName("id_library")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Library)
                .WithMany(x => x.LibraryBooks)
                .HasForeignKey(x => x.IdLibrary)
                .HasConstraintName("fk_library_book_library_id_library")
                .OnDelete(DeleteBehavior.Restrict);
            
            o.Property(x => x.IdContact)
                .HasColumnName("id_contact")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Contact)
                .WithMany(x => x.LibraryBooks)
                .HasForeignKey(x => x.IdContact)
                .HasConstraintName("fk_library_book_contact_id_contact")
                .OnDelete(DeleteBehavior.Restrict);

            o.HasMany(x => x.Images)
                .WithOne(x => x.LibraryBook)
                .HasForeignKey(x => x.IdLibraryBook)
                .HasConstraintName("fk_library_book_image_id_library_book")
                .OnDelete(DeleteBehavior.Restrict);
            
            o.Property(x => x.IdBook)
                .HasColumnName("id_book")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Book)
                .WithMany(x => x.LibraryBooks)
                .HasForeignKey(x => x.IdBook)
                .HasConstraintName("fk_library_book_book_id_book")
                .OnDelete(DeleteBehavior.Restrict);                        
                                    
            o.Property(x => x.Reating)
                .HasColumnName("reating")
                .HasColumnType("varchar(255)")
                .IsRequired(false);
            
            o.Property(x => x.Active)
                .HasColumnName("active")
                .HasColumnType("boolean")
                .IsRequired();
            
            o.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();

            o.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}