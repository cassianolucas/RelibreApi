using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class SubscriptionMap : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> o)
        {
            o.ToTable("subscription");

            o.Property(x => x.Id)
                .HasColumnName("id")
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

             o.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)")
                .IsRequired();

            o.Property(x => x.Value)
                .HasColumnName("value")
                .HasColumnType("numeric(12,4)")
                .IsRequired();

            o.Property(x => x.Period)
                .HasColumnName("period")
                .HasColumnType("integer")
                .IsRequired();

            o.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}