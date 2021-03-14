using Microsoft.AspNetCore.Identity;

namespace Game_Step.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
