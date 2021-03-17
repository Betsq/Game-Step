using System.ComponentModel.DataAnnotations;

namespace Game_Step.IdentityViewModels
{
    public class AccountChangePasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
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
