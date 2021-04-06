using Game_Step.Models.Comments;
using Game_Step.Models.GamesModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameKey> GameKeys { get; set; }
        public DbSet<GamePrice> GamePrices { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<GameScreenshot> GameScreenshots { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            :base(contextOptions)
        {
            
        }
    }
}
