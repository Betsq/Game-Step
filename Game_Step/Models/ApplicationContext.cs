using Microsoft.EntityFrameworkCore;

namespace Game_Step.Models
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<GameKey> GameKeys { get; set; }
        public DbSet<Region> Regions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            :base(contextOptions)
        {
            
        }
    }
}
