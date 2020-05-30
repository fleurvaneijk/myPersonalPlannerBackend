using Microsoft.EntityFrameworkCore;

namespace MyPersonalPlannerBackend.Model
{
    public class MariaDBContext : DbContext
    {

        public MariaDBContext(DbContextOptions<MariaDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();
        }
    }
}
