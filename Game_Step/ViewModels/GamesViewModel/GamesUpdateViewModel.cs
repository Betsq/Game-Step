using Microsoft.AspNetCore.Http;

namespace Game_Step.ViewModels
{
    public class GamesUpdateViewModel : GamesCreateViewModel
    {
        public string MainImagePath { get; set; }
        public string InnerImagePath { get; set; }
        public string ImageInCatalogPath { get; set; }

        public IFormFile Screenshot { get; set; } 
    }
}
