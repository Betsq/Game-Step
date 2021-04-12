using System;
using System.Collections.Generic;

namespace Game_Step.Models
{
    public class OrderNumber
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int TotalPrice { get; set; }

        public bool UserAgreement { get; set; }

        public string PaymentMethod { get; set; }

        public string Promocode { get; set; }

        public DateTime OrderTime { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
