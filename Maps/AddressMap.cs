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
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            o.Property(x => x.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Person)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.IdPerson)
                .HasConstraintName("fk_adress_person_id_person")
                .OnDelete(DeleteBehavior.Restrict);

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

            o.Property(x => x.FullAddress)
                .HasColumnName("full_address")
                .HasColumnType("varchar")
                .IsRequired();
                        
            o.Property(x => x.Master)
                .HasColumnName("master")
                .HasColumnType("boolean")
                .IsRequired();

            o.Property(x => x.ZipCode)
                .HasColumnName("zip_code")
                .HasColumnType("varchar(8)")
                .IsRequired(false);

            o.Property(x => x.Street)
                .HasColumnName("street")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("varchar(144)")
                .IsRequired(false);

            o.Property(x => x.Neighborhood)
                .HasColumnName("neighborhood")
                .HasColumnType("varchar(255)")
                .IsRequired();

            o.Property(x => x.City)
                .HasColumnName("city")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.State)                
                .HasColumnName("state")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.Complement)
                .HasColumnName("complement")
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