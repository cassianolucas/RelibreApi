using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class PessoaMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")                
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();
            
            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar")
                .HasMaxLength(255);        
            
            builder.Property(x => x.Document)
                .HasColumnName("document")
                .HasColumnType("varchar")
                .HasMaxLength(18);

            builder.Property(x => x.PersonType)
                .HasColumnName("type_person")
                .HasMaxLength(144);

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