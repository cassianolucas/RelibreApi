using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> o)
        {
            o.ToTable("image");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                // .UseSerialColumn<long>()                
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            o.Property(x => x.ImageLink)
                .HasColumnName("image_link")
                .HasColumnType("varchar(255)")
                .IsRequired();
            
            o.Property(x => x.IdLibraryBook)
                .HasColumnName("id_library_book")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.LibraryBook)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.IdLibraryBook)
                .HasConstraintName("fk_image_library_book_id_library_book")
                .OnDelete(DeleteBehavior.Restrict);            
        }
    }
}