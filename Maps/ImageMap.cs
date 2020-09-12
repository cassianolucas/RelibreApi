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
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Thumbnail)
                .HasColumnName("thumbnail")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.Active)
                .HasColumnName("active")
                .HasColumnType("boolean")
                .IsRequired();
            
            o.Property(x => x.Small)
                .HasColumnName("small")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.Medium)
                .HasColumnName("medium")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.Large)
                .HasColumnName("large")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.SmallThumbnail)
                .HasColumnName("small_thumbnail")
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            o.Property(x => x.ExtraLarge)
                .HasColumnName("extra_large")
                .HasColumnType("varchar(255)")
                .IsRequired(false);
            
            o.Property(x => x.IdLibraryBook)
                .HasColumnName("id_library_book")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.LibraryBook)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.IdLibraryBook)
                .HasConstraintName("fk_image_library_book_id_library_book")
                .OnDelete(DeleteBehavior.Restrict);

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