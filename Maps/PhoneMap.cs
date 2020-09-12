using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class PhoneMap : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> o)
        {
            o.ToTable("phone");

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

            o.HasOne(x => x.Person)
                .WithMany(x => x.Phones)
                .HasForeignKey(x => x.IdPerson)
                .HasConstraintName("fk_phone_person_id_person")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("varchar(50)")
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