using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> o)
        {
            o.ToTable("notification");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                // .UseSerialColumn<long>()                
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            o.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();
            
            o.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();            
        }
    }
}