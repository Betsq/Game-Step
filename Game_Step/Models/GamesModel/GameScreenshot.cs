using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.GamesModel
{
    public class GameScreenshot
    {
        public int Id { get; set; }
        public string Screenshot { get; set; }
        public string Trailer { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
