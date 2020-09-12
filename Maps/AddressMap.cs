using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> o)
        {
            o.ToTable("address");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            o.Property(x => x.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("bigint")
                .IsRequired();

            o.Property(x => x.NickName)
                .HasColumnName("nick_name")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            o.Property(x => x.Latitude)
                .HasColumnName("latitude")
                .HasColumnType("varchar(25)")
                .IsRequired();
            
            o.Property(x => x.Longitude)
                .HasColumnName("longitude")
                .HasColumnType("varchar(25)")
                .IsRequired();
                        
            o.Property(x => x.Master)
                .HasColumnName("master")
                .HasColumnType("boolean")
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