using Game_Step.Models.GamesModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Game_Step.ViewModels.GamesViewModel
{
    public class GamesScreenshotViewModel
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string PathImage { get; set; }
        public IFormFileCollection Screenshots { get; set; }
        public List<GameScreenshot> GameScreenshotsList = new List<GameScreenshot>();
    }
}
