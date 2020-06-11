using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class PhoneMap : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("phone");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")                
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.IdPerson)
                .HasColumnName("id_person")                
                .IsRequired();

            builder.Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();
            
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