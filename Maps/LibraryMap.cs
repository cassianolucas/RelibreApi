using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class LibraryMap : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> o)
        {
            o.ToTable("library");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                // .UseSerialColumn<long>()                
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Person)
                .WithOne(x => x.Library)           
                .HasForeignKey<Library>(x => x.IdPerson)
                .HasConstraintName("fk_library_person_id_person")
                .OnDelete(DeleteBehavior.Restrict);

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