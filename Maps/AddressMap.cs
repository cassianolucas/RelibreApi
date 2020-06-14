using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.Property(x => x.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.NickName)
                .HasColumnName("nick_name")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            // builder.Property(x => x.PostalCode)                
            //     .HasColumnName("postal_code")
            //     .HasColumnType("varchar")
            //     .HasMaxLength(8)
            //     .IsRequired();
            
            builder.Property(x => x.Master)
                .HasColumnName("master")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(x => x.State)
                .HasColumnName("state")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();            
        }
    }
}