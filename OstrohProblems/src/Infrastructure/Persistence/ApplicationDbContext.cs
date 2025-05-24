using System.Reflection;
using Application.Services.HashPasswordService;
using Domain.Categories;
using Domain.Comments;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Ratings;
using Domain.RefreshTokens;
using Domain.Statuses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHashPasswordService hashPasswordService) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            DataSeed.Seed(builder, hashPasswordService);
        }
    }
}