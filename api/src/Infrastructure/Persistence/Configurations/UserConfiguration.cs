using Domain.Identity.Roles;
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

        builder.Property(p => p.ClerkId)
            .HasMaxLength(100);

        builder.HasIndex(u => u.ClerkId)
            .IsUnique();
        
        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(p => p.Name)
            .HasMaxLength(50); 

        builder.Property(p => p.Surname)
            .HasMaxLength(50); 

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20); 

        builder.Property(p => p.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.RoleId)
            .HasConversion(id => id.Value, value => new RoleId(value))
            .IsRequired();

        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(u => u.Problems)
            .WithOne(p => p.CreatedBy)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

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
