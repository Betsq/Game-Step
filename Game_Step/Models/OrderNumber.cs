using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models
{
    public class OrderNumber
    {
        public int Id { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
