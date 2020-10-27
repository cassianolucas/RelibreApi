using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelibreApi.Models;

namespace RelibreApi.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> o)
        {
            o.ToTable("user");

            o.HasKey(x => x.Login);

            o.HasIndex(x => x.Login).IsUnique();

            o.Property(x => x.Login)
                .HasColumnName("login")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            o.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            o.Property(x => x.IdProfile)
                .HasColumnName("id_profile")
                .HasColumnType("bigint")
                .IsRequired();
            
            o.HasOne(x => x.Profile)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.IdProfile)
                .HasConstraintName("fk_user_profile_id_profile")
                .OnDelete(DeleteBehavior.Restrict);
            
            o.Property(x => x.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("bigint")
                .IsRequired();

            o.Property(x => x.TotalCount)
                .HasColumnName("total_count")
                .HasColumnType("integer")
                .IsRequired();

            o.Property(x => x.TotalValue)
                .HasColumnType("total_value")
                .HasColumnType("integer")
                .IsRequired();
            
            o.HasOne(x => x.Person)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.IdPerson)
                .HasConstraintName("fk_user_person_id_person")
                .OnDelete(DeleteBehavior.Restrict);

            o.Property(x => x.LoginVerified)
                .HasColumnName("login_verified")
                .HasColumnType("boolean")
                .IsRequired();
        }
    }
}