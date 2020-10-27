using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> o)
        {
            o.ToTable("person");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")                
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();
            
            o.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar")
                .HasMaxLength(255);        
            
            o.Property(x => x.Document)
                .HasColumnName("document")
                .HasColumnType("varchar")
                .HasMaxLength(18);

            o.Property(x => x.PersonType)
                .HasColumnName("type_person")
                .HasMaxLength(2)
                .IsRequired();

            o.Property(x => x.Active)
                .HasColumnName("active")
                .HasColumnType("boolean")
                .IsRequired();

            o.Property(x => x.BirthDate)
                .HasColumnName("birth_date")
                .HasColumnType("timestamp")
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