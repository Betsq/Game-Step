using Microsoft.EntityFrameworkCore;

namespace Game_Step.Models
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            :base(contextOptions)
        {

        }
    }
}
