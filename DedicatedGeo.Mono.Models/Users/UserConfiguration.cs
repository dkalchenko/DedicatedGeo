using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.HasIndex(x => x.Login)
            .IsUnique();
        builder.HasIndex(x => x.Password);
        builder.HasIndex(x => new { x.CountryCode, x.NationalNumber })
            .IsUnique();
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.HasIndex(x => x.FirstName);
        builder.HasIndex(x => x.SecondName);

        builder.Property(x => x.UserId)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Email)
            .HasMaxLength(MySqlConstants.EmailMaxLength);
        builder.Property(x => x.Password)
            .HasMaxLength(MySqlConstants.PasswordMaxLength);
        builder.Property(x => x.Login)
            .HasMaxLength(MySqlConstants.LoginMaxLength);
        builder.Property(x => x.NationalNumber);
        builder.Property(x => x.CountryCode);
        builder.Property(x => x.Type);
        builder.Property(x => x.Role)
            .HasMaxLength(MySqlConstants.RoleMaxLength);
        builder.Property(x => x.FirstName)
            .HasMaxLength(MySqlConstants.FirstNameMaxLength);
        builder.Property(x => x.SecondName)
            .HasMaxLength(MySqlConstants.SecondNameMaxLength);
    }
}