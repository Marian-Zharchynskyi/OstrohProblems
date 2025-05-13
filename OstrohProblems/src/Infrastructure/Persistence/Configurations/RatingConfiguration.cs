using Domain.Ratings;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => new RatingId(value))
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.Points)
            .IsRequired()
            .HasColumnType("decimal(3,2)"); 

        builder.ToTable(t => 
            t.HasCheckConstraint("CK_Rating_Points_Range", "\"Points\" >= 1.00 AND \"Points\" <= 5.00"));

        builder.Property(r => r.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        
        builder.HasOne(r => r.Problem)
            .WithMany(p => p.Ratings) 
            .HasForeignKey(r => r.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}