using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedicatedGeo.Mono.Models.User;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Role);
        builder.HasIndex(x => new {x.Email, x.Role});
        
        builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(512);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}