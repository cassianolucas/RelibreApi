using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class AccessProfileMap : IEntityTypeConfiguration<AccessProfile>
    {
        public void Configure(EntityTypeBuilder<AccessProfile> o)
        {
            o.ToTable("access_profile");

            o.HasKey(x => x.Id);

            o.Property(x => x.Id)
                .HasColumnName("id")
                .UseSerialColumn<long>()
                .HasIdentityOptions(1, 1, 1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            o.Property(x => x.Access)
                .HasColumnName("access")
                .HasColumnType("varchar(144)")
                .IsRequired();
            
            o.Property(x => x.IdProfile)
                .HasColumnName("id_profile")
                .HasColumnType("bigint")
                .IsRequired();

            o.HasOne(x => x.Profile)
                .WithMany(x => x.AccessProfiles)
                .HasForeignKey(x => x.IdProfile)
                .HasConstraintName("fk_profile_access_profile_id_profile")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}