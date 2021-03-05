using Microsoft.EntityFrameworkCore;

namespace Game_Step.Models
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<MinimumSystemRequirements> MinimumSystemRequirements { get; set; }
        public DbSet<RecommendedSystemRequirements> RecommendedSystemRequirements { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            :base(contextOptions)
        {
            
        }
    }
}
