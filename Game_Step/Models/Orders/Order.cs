using System.Collections.Generic;

namespace Game_Step.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int AmountProduct { get; set; }

        public int OrderNumberId { get; set; }
        public OrderNumber OrderNumber { get; set; }
    }
}
