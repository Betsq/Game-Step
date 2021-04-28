using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.GamesModel
{
    public class GameTags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameParam { get; set; }
        public bool Show { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
