using Domain.Problems;
using Microsoft.EntityFrameworkCore;
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
        
        builder.HasOne(ps => ps.ProblemStatus)
            .WithMany(pr => pr.Problems)
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Problem)
            .HasForeignKey(c => c.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Problems)
            .UsingEntity(j => j.ToTable("fk_problem_categories"));
    }
}