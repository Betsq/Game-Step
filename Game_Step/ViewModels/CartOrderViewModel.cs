using Game_Step.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class CartOrderViewModel
    {
        public List<Cart> InCart { get; set; }

        public string Email { get; set; }

        public bool UserAgreement { get; set; }

        public string PaymentMethod { get; set; }

        public string Promocode { get; set; }

        public Dictionary<int, int> Items { get; set; } = new Dictionary<int, int>();
    }
}
