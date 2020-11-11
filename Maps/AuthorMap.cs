using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> o)
        {
            o.ToTable("author");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                // .UseSerialColumn<long>()                
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(255)")
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