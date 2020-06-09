using Microsoft.EntityFrameworkCore;

namespace MyPersonalPlannerBackend.Model
{
    public class MariaDBContext : DbContext
    {

        public MariaDBContext(DbContextOptions<MariaDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<PlannerItem> PlannerItems { get; set; }
        public DbSet<PlannerUser> PlannerUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();

            modelBuilder.Entity<Planner>()
                .HasIndex(planner => planner.Id)
                .IsUnique();

            modelBuilder.Entity<PlannerItem>()
                .HasIndex(item => item.Id)
                .IsUnique();

            modelBuilder.Entity<PlannerUser>()
                .HasKey(pu => new { pu.PlannerId, pu.UserId });


            modelBuilder.Entity<PlannerItem>()
                .HasOne(i => i.Planner)
                .WithMany(p => p.PlannerItems)
                .HasForeignKey(i => i.PlannerId);
        }
    }
}
