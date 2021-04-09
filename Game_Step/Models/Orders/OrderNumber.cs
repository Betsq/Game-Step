using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.Orders
{
    public class OrderNumber
    {
        public int id { get; set; }

        public List<Order> Orders { get; set; }
        
        public Order Order { get; set; }
    }
}
