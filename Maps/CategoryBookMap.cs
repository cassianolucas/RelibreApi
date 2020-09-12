using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class CategoryBookMap : IEntityTypeConfiguration<CategoryBook>
    {
        public void Configure(EntityTypeBuilder<CategoryBook> o)
        {
            o.ToTable("category_book");

            o.HasKey(x => new {x.IdBook, x.IdCategory});

            o.HasOne(x => x.Book)
                .WithMany(x => x.CategoryBooks)
                .HasForeignKey(x => x.IdBook)
                .HasConstraintName("fk_category_book_book_id_book")
                .OnDelete(DeleteBehavior.Restrict);
            
            o.HasOne(x => x.Category)
                .WithMany(x => x.CategoryBooks)
                .HasForeignKey(x => x.IdCategory)
                .HasConstraintName("fk_category_book_category_id_category")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}