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
        public List<Game> Games { get; set; }
        public List<Category> Categories { get; set; }
    }
}
