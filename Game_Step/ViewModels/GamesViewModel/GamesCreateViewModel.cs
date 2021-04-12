using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Game_Step.ViewModels
{
    public class GamesCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public bool IsDiscount { get; set; }

        public int Discount { get; set; }

        public string Description { get; set; }

        public IFormFile MainImage { get; set; }
        public IFormFile InnerImage { get; set; }
        public IFormFile ImageInCatalog { get; set; }

        [Required]
        public int QuantityOfGoods { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string Publisher { get; set; }

        public string Developer { get; set; }

        public string Features { get; set; }

        public string Region { get; set; }

        public string WhereKeyActivated { get; set; }

        public string RecommendOC { get; set; }

        public string RecommendCPU { get; set; }

        public string RecommendRAM { get; set; }

        public string RecommendVideoCard { get; set; }

        public string RecommendDirectX { get; set; }

        public string RecommendHDD { get; set; }

        public string MinimumOC { get; set; }

        public string MinimumCPU { get; set; }

        public string MinimumRAM { get; set; }

        public string MinimumVideoCard { get; set; }

        public string MinimumDirectX { get; set; }

        public string MinimumHDD { get; set; }
    }
}
