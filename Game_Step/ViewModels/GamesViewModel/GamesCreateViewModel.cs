using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Game_Step.Models;
using Game_Step.Models.GamesModel;

namespace Game_Step.ViewModels
{
    public class GamesCreateViewModel
    {
        public Game Game { get; set; }
        public GamePrice Price { get; set; }
        public GameRecommendation GameRecommendation { get; set; }
        public GameMinimum GameMinimum { get; set; }
        public IFormFile MainImage { get; set; }
        public IFormFile InnerImage { get; set; }
        public IFormFile ImageInCatalog { get; set; }
    }
}
