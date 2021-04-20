using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.GamesModel
{
    public class GameMinimum
    {
        public int Id { get; set; }
        public string MinimumOC { get; set; }
        public string MinimumCPU { get; set; }
        public string MinimumRAM { get; set; }
        public string MinimumVideoCard { get; set; }
        public string MinimumDirectX { get; set; }
        public string MinimumHDD { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
    }
}
