using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game_Step.Models;
using Game_Step.Models.Slider;
using Microsoft.AspNetCore.Http;

namespace Game_Step.ViewModels
{
    public class SliderViewModel
    {
        public MainItemSlider MainSlider { get; set; }
        public IFormFile MainSliderImage { get; set; }
        public AdditionalItemSlider SecondSlider { get; set; }
        public IFormFile SecondSliderImage { get; set; }
        public AdditionalItemSlider ThirtySlider { get; set; }
        public IFormFile ThirtySliderImage { get; set; }
        public List<Game> Game { get; set; }

        public static implicit operator SliderViewModel(List<AdditionalItemSlider> v)
        {
            throw new NotImplementedException();
        }
    }
}
