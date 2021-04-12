using Game_Step.Models;
using System.Collections.Generic;

namespace Game_Step.ViewModels
{
    public class HomePageViewModel
    {
        public List<Game> Games { get; set; }
        public List<Category> Categories { get; set; }
    }
}
