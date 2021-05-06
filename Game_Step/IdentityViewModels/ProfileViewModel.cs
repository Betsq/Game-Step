using System.ComponentModel.DataAnnotations;
using Game_Step.Models;
using Microsoft.AspNetCore.Http;

namespace Game_Step.IdentityViewModels
{
    public class ProfileViewModel : User
    {
        public IFormFile AvatarFormFile { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        
        [Display(Name = "Email")]
        public string MyEmail { get; set; }

        [Compare("MyEmail", ErrorMessage = "Email mismatch")]
        [Display(Name = "Email")]
        public string ConfirmEmail { get; set; }
    }
}
