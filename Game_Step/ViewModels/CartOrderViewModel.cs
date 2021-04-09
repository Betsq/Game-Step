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

        public OrderNumber OrderNumber { get; set; }
    }
}
