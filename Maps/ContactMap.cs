using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> o)
        {
            o.ToTable("contact");

            o.HasKey(x => x.Id);

            o.HasIndex(x => x.Email)
                .IsUnique();

            o.Property(x => x.Id)
                .HasColumnName("id")
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Name) 
                .HasColumnName("name")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            o.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();            

            o.Property(x => x.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar(50)")
                .IsRequired();

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