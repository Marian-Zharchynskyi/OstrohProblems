using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProblemImageConfiguration : IEntityTypeConfiguration<ProblemImage>
{
    public void Configure(EntityTypeBuilder<ProblemImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProblemImageId(x));

        builder.Property(x => x.ProblemId).HasConversion(x => x.Value, x => new ProblemId(x));

        builder.HasOne(x => x.Problem)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ProblemId)
            .HasConstraintName("fk_problem_images_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}