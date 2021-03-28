using Game_Step.Models;
using Game_Step.Models.GamesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class HomePageViewModel
    {
        public Game game { get; set; }
        public GamePrice gamePrice { get; set; }
    }
}
