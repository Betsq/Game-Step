using System.ComponentModel.DataAnnotations;

namespace Game_Step.IdentityViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
