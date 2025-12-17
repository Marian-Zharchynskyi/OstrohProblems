using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CoordinatorImageConfiguration : IEntityTypeConfiguration<CoordinatorImage>
{
    public void Configure(EntityTypeBuilder<CoordinatorImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new CoordinatorImageId(x));

        builder.Property(x => x.ProblemId).HasConversion(x => x.Value, x => new ProblemId(x));

        builder.HasOne(x => x.Problem)
            .WithMany(x => x.CoordinatorImages)
            .HasForeignKey(x => x.ProblemId)
            .HasConstraintName("fk_problem_coordinator_images_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
