using Game_Step.Models.Comments;
using Game_Step.Models.GamesModel;
using System;
using System.Collections.Generic;

namespace Game_Step.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Features { get; set; }
        public string Region { get; set; }
        public string WhereKeyActivated { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GamePrice GamePrice { get; set; }
        public GameImage GameImage { get; set; }
        public GameRecommendation Recommendation { get; set; }
        public GameMinimum Minimum { get; set; }
        public List<GameTags> GameTags { get; set; }
        public List<GameScreenshot> GameScreenshots { get; set; } 
        public List<GameKey> GameKeys { get; set; }
        public List<MainComment> MainComments { get; set; }
    }
}
