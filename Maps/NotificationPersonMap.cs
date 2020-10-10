using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class NotificationPersonMap : IEntityTypeConfiguration<NotificationPerson>
    {
        public void Configure(EntityTypeBuilder<NotificationPerson> o)
        {
            o.ToTable("notification_person");

            o.HasKey(x => x.Id);

            // o.Property(x => x.Id)
            //     .HasColumnName("id")
            //     .UseSerialColumn<long>()
            //     .HasIdentityOptions(1, 1, 1)
            //     .ValueGeneratedOnAdd()
            //     .IsRequired();

            o.Property(x => x.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.Property(x => x.IdNotification)
                .HasColumnName("id_notification")
                .HasColumnType("bigint")
                .IsRequired();

            o.Property(x => x.Active)
                .HasColumnName("active")
                .HasColumnType("boolean")
                .IsRequired();
            
            o.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();

            o.HasOne(x => x.Notification)
                .WithMany(x => x.NotificationPeople)
                .HasForeignKey(x => x.IdNotification)
                .HasConstraintName("fk_notification_person_notification_id_notification")
                .OnDelete(DeleteBehavior.Restrict);

            o.HasOne(x => x.Person)
                .WithMany(x => x.NotificationPeople)
                .HasForeignKey(x => x.IdPerson)
                .HasConstraintName("fk_notification_person_person_id_person")
                .OnDelete(DeleteBehavior.Restrict);                
        }
    }
}