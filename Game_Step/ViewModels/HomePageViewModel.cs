using Game_Step.Models;
using System.Collections.Generic;
using Game_Step.Models.Slider;

namespace Game_Step.ViewModels
{
    public class HomePageViewModel
    {
        public List<Game> Games { get; set; }
        public List<Category> Categories { get; set; }
        public List<MainItemSlider> Sliders { get; set; }
    }
}
