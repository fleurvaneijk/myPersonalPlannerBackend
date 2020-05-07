using Microsoft.EntityFrameworkCore;

namespace MyPersonalPlannerBackend.Model
{
    public class MariaDBContext : DbContext
    {

        public MariaDBContext(DbContextOptions<MariaDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
