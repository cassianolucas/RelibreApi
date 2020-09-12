using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> o)
        {
            o.ToTable("book");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();
            
            o.Property(x => x.CodeIntegration)
                .HasColumnName("code_integration")
                .HasColumnType("varchar(255)")
                .IsRequired(false);
            
            o.Property(x => x.Isbn13)
                .HasColumnName("isbn13")
                .HasColumnType("varchar(13)")
                .IsRequired(false);
            
            o.Property(x => x.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(144)")
                .IsRequired();
            
            o.Property(x => x.AverageRating)
                .HasColumnName("average_rating")
                .HasColumnType("varchar(144)")
                .IsRequired(false);
            
            o.Property(x => x.MaturityRating)
                .HasColumnName("maturity_rating")
                .HasColumnType("varchar(144)")
                .IsRequired(false);
            
            o.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}