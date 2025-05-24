using Domain.Comments;
using Domain.Problems;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => new CommentId(value))
            .IsRequired();

        builder.Property(c => c.Content)
            .IsRequired()
            .HasColumnType("varchar(300)");

        builder.Property(c => c.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");

        builder.Property(c => c.UpdatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");

        builder.HasOne(c => c.Problem)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}