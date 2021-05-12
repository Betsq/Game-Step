using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Game_Step.Models.Slider
{
    public class MainItemSlider
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string Link { get; set; }
        public bool IsGame { get; set; }
        public int Discount { get; set; }
        public int OldPrice { get; set; }
        public int CurrentPrice { get; set; }

    }
}
