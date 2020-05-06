using Microsoft.EntityFrameworkCore;

namespace MyPersonalPlannerBackend.Model
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("Server=localhost; Database=mypersonalplanner;User=root;Password=password");
    }
}
