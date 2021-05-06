using Microsoft.AspNetCore.Identity;

namespace Game_Step.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
    }
}
