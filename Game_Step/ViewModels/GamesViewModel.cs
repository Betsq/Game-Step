using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class GamesViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

        public IFormFile Image { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Article { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Features { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string WhereKeyActivated { get; set; }

        [Required]
        public string RecommendOC { get; set; }
        [Required]
        public string RecommendCPU { get; set; }
        [Required]
        public string RecommendRAM { get; set; }
        [Required]
        public string RecommendVideoCard { get; set; }
        [Required]
        public string RecommendDirectX { get; set; }
        [Required]
        public string RecommendHDD { get; set; }
        [Required]
        public string MinimumOC { get; set; }
        [Required]
        public string MinimumCPU { get; set; }
        [Required]
        public string MinimumRAM { get; set; }
        [Required]
        public string MinimumVideoCard { get; set; }
        [Required]
        public string MinimumDirectX { get; set; }
        [Required]
        public string MinimumHDD { get; set; }
    }
}
