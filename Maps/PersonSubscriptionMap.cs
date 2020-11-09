using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class PersonSubscriptionMap : IEntityTypeConfiguration<PersonSubscription>
    {
        public void Configure(EntityTypeBuilder<PersonSubscription> o)
        {
            o.ToTable("person_subscription");

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
                .WithMany(x => x.PersonSubscriptions)
                .HasForeignKey(x => x.IdPerson)
                .HasConstraintName("fk_person_subscription_id_person")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.IdSubscription)
                .HasColumnType("id_subscription")
                .HasColumnType("bigint")
                .IsRequired();

            o.HasOne(x => x.Subscription)
                .WithMany(x => x.PersonSubscriptions)
                .HasForeignKey(x => x.IdSubscription)
                .HasConstraintName("fk_subscription_subscription_id_subscription")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.PaidAt)
                .HasColumnName("paid_at")
                .HasColumnType("timestamp")
                .IsRequired();

            o.Property(x => x.Validate)
                .HasColumnName("validate")
                .HasColumnType("varchar(255)");                

            o.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}