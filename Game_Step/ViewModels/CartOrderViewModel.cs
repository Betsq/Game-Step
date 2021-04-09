using Game_Step.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class CartOrderViewModel
    {
        public List<Cart> InCart { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Email mismatch")]
        public string EmailComapre { get; set; }

        [Required]
        public bool UserAgreement { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string Promocode { get; set; }

        [Required]
        public Dictionary<int, int> Items { get; set; } = new Dictionary<int, int>();
    }
}
