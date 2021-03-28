using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsDiscount { get; set; }
        public int Discount { get; set; }
        public int DiscountPrice { get; set; }
        public int Quantity { get; set; }
    }
}
