using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Game_Step.Models;
using Game_Step.Models.GamesModel;
using Game_Step.ViewModels.GamesViewModel;

namespace Game_Step.ViewModels
{
    public class GamesCreateViewModel
    {
        public Game Game { get; set; }
        public GamePrice Price { get; set; }
        public GameRecommendation GameRecommendation { get; set; }
        public GameMinimum GameMinimum { get; set; }
        public List<Tag> Tags { get; set; }
        public Dictionary<int, GamesTagsViewModel> TagsDictionary { get; set; }
        public IFormFile MainImage { get; set; }
        public IFormFile InnerImage { get; set; }
        public IFormFile ImageInCatalog { get; set; }
    }
}
