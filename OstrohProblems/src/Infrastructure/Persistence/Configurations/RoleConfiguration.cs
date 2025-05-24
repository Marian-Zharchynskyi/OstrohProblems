using Domain.Identity.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                .HasConversion(
                    id => id.Value,
                    value => new RoleId(value))
                .IsRequired();

            builder.Property(ps => ps.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}