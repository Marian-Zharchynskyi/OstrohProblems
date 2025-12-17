using System.Text.Json;
using Domain.Identity.Users;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => new ProblemId(value))
            .IsRequired();

        builder.Property(p => p.CreatedById)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder.HasOne(p => p.CreatedBy)
            .WithMany(u => u.Problems)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.CoordinatorId)
            .HasConversion(
                id => id != null ? id.Value : Guid.Empty,
                value => value == Guid.Empty ? null : new UserId(value));

        builder.HasOne(p => p.Coordinator)
            .WithMany()
            .HasForeignKey(p => p.CoordinatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasColumnType("varchar(100)");
        
        builder.Property(p => p.Longitude)
            .IsRequired()
            .HasColumnType("double precision");
        
        builder.Property(p => p.Latitude)
            .IsRequired()
            .HasColumnType("double precision");

        builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(300)");
        
        builder.Property(p => p.Status)
            .HasConversion(
                status => status.Value,
                value => ProblemStatus.From(value))
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(p => p.CurrentState)
            .HasColumnType("varchar(2000)");

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Problem)
            .HasForeignKey(c => c.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        var categoriesComparer = new ValueComparer<List<Category>>(
            (c1, c2) => ReferenceEquals(c1, c2) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
            c => c == null
                ? 0
                : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Value.GetHashCode())),
            c => c == null ? new List<Category>() : c.ToList());

        var categoriesProperty = builder.Property(p => p.Categories)
            .HasConversion(
                v => JsonSerializer.Serialize(v.Select(c => c.Value).ToList(), (JsonSerializerOptions?)null),
                v => (JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
                    .Select(Category.From).ToList())
            .HasColumnType("jsonb");

        categoriesProperty.Metadata.SetValueComparer(categoriesComparer);
    }
}