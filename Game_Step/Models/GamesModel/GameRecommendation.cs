using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.GamesModel
{
    public class GameRecommendation
    {
        public int Id { get; set; }
        public string RecommendOC { get; set; }
        public string RecommendCPU { get; set; }
        public string RecommendRAM { get; set; }
        public string RecommendVideoCard { get; set; }
        public string RecommendDirectX { get; set; }
        public string RecommendHDD { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
    }
}
