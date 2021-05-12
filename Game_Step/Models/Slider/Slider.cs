using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.Slider
{
    public class Slider
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string Link { get; set; }
        public bool IsGame { get; set; }
        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
}
