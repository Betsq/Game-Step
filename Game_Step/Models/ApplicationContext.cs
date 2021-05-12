using Game_Step.Models.Comments;
using Game_Step.Models.GamesModel;
using Game_Step.Models.Orders;
using Game_Step.Models.Slider;
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
        public DbSet<GameMinimum> GameMinimums { get; set; }
        public DbSet<GameRecommendation> GameRecommendations { get; set; }
        public DbSet<GameTags> GameTags { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderNumber> OrderNumbers { get; set; }
        public DbSet<OrderKeysGame> OrderKeysGames { get; set; }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<MainItemSlider> MainItemSliders { get; set; }
        public DbSet<AdditionalItemSlider> AdditionalItemSliders { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            :base(contextOptions)
        {
            
        }
    }
}
