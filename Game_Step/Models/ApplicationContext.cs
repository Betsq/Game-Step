using Microsoft.EntityFrameworkCore;

namespace Game_Step.Models
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            :base(contextOptions)
        {
            
        }
    }
}
