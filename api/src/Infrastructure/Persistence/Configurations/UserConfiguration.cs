using Domain.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(p => p.Value, value => new UserId(value));
        
        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(p => p.FullName)
            .HasMaxLength(100); 

        builder.Property(p => p.PasswordHash)
            .IsRequired()
            .HasMaxLength(255); 

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity(j => j.ToTable("fk_user_roles"));
        
        builder.HasMany(p => p.Problems)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Comments)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Ratings)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
