using Domain.Statuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                .HasConversion(
                    id => id.Value,
                    value => new StatusId(value))
                .IsRequired();

            builder.Property(ps => ps.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}