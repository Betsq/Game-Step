using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class CatalogViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string KeyActivate { get; set; }
        public string Genre { get; set; }
        public bool IsDiscount { get; set; }
        public int Discount { get; set; }
        public int PriceDiscount { get; set; }
        public int Price { get; set; }

    }
}
