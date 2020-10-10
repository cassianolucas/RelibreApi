using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class ContactBookMap : IEntityTypeConfiguration<ContactBook>
    {
        public void Configure(EntityTypeBuilder<ContactBook> o)
        {
            o.ToTable("contact_book");

            o.HasKey(x => new { x.IdContactOwner, x.IdContactRequest, x.IdLibraryBook });

            o.Property(x => x.IdContactOwner)
                .HasColumnName("id_contact_owner")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.ContactOwner)
                .WithMany(x => x.ContactBooksOwner)
                .HasForeignKey(x => x.IdContactOwner)
                .HasConstraintName("fk_contact_book_owner_contact_id_contact")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.IdContactRequest)
                .HasColumnName("id_contact_request")
                .HasColumnType("bigint")
                .IsRequired();

            o.HasOne(x => x.ContactRequest)
                .WithMany(x => x.ContactBooksRequest)
                .HasForeignKey(x => x.IdContactRequest)
                .HasConstraintName("fk_contact_book_request_contact_id_contact")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.IdLibraryBook)
                .HasColumnName("id_library_book")
                .HasColumnType("bigint")
                .IsRequired();

            o.HasOne(x => x.LibraryBook)
                .WithMany(x => x.ContactBooks)
                .HasForeignKey(x => x.IdLibraryBook)
                .HasConstraintName("fk_contact_book_library_book_id_library_book")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.Available)
                .HasColumnName("available")
                .HasColumnType("boolean")
                .IsRequired();
        }
    }
}