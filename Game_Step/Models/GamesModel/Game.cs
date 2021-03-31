using Game_Step.Models.GamesModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Game_Step.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuantityOfGoods { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Features { get; set; }
        public string Region { get; set; }
        public string WhereKeyActivated { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte[] Image { get; set; }
        public GamePrice GamePrice { get; set; }
        public GameImage GameImage { get; set; }
        public List<GameScreenshot> GameScreenshots { get; set; } = new List<GameScreenshot>();
        public List<GameKey> GameKeys { get; set; } = new List<GameKey>();

        public string RecommendOC { get; set; }
        public string RecommendCPU { get; set; }
        public string RecommendRAM { get; set; }
        public string RecommendVideoCard { get; set; }
        public string RecommendDirectX { get; set; }
        public string RecommendHDD { get; set; }
        public string MinimumOC { get; set; }
        public string MinimumCPU { get; set; }
        public string MinimumRAM { get; set; }
        public string MinimumVideoCard { get; set; }
        public string MinimumDirectX { get; set; }
        public string MinimumHDD { get; set; }
    }
}
