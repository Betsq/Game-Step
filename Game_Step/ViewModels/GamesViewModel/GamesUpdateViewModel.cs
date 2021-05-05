using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Game_Step.ViewModels
{
    public class GamesUpdateViewModel : GamesCreateViewModel
    {
        public int Id { get; set; }
        public string MainImagePath { get; set; }
        public string InnerImagePath { get; set; }
        public string ImageInCatalogPath { get; set; }
        public List<int> GameTagsId { get; set; }
    }
}
