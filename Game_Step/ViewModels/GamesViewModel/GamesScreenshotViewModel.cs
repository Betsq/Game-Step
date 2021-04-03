using Game_Step.Models.GamesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels.GamesViewModel
{
    public class GamesScreenshotViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PathImage { get; set; }
        public List<GameScreenshot> GameScreenshotsList = new List<GameScreenshot>();
    }
}
