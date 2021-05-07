using Game_Step.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Game_Step.ViewModels
{
    public class CartOrderViewModel
    {
        public List<Game> GameInCart { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Email mismatch")]
        public string EmailCompare { get; set; }

        [Required]
        public bool UserAgreement { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string Promocode { get; set; }

        [Required]
        public Dictionary<int, int> Items { get; set; }
    }
}
