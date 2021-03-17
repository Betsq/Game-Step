using System.ComponentModel.DataAnnotations;

namespace Game_Step.IdentityViewModels
{
    public class AccountChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        public string OldPassword { get; set; }

        
    }
}
