using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Game_Step.Models
{
    public class User : IdentityUser
    {
        [StringLength(20, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 2)]
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
    }
}
