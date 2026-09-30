using csm_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace csm_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostModel>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.HasPostgresEnum<UserRole>();
            modelBuilder.HasPostgresEnum<PostStatus>();
        }
    }
}