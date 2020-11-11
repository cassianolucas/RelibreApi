using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class ProfileMap : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> o)
        {
            o.ToTable("profile");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                // .UseSerialColumn<long>()                
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            o.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(144)")
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