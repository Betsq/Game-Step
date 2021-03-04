using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models
{
    public class RecommendedSystemRequirements
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string OC { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string VideoCard { get; set; }
        public string DirectX { get; set; }
        public string HDD { get; set; }
    }
}
