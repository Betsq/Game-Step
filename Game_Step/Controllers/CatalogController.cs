using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.CatalogController
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public CatalogController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet("{countPag}")]
        public IEnumerable<CatalogViewModel> Get(int countPag)
        {
            int size = 8;

            var game = _db.Games
                    .Include(price => price.GamePrice)
                    .Include(img => img.GameImage)
                    .Skip((countPag - 1) * size).Take(size).ToList();

            return game.Select(g => new CatalogViewModel()
            {
                Id = g.Id,
                Image = g.GameImage.ImageInCatalog,
                Name = g.Name,
                KeyActivate = g.WhereKeyActivated,
                Genre = g.Genre,
                IsDiscount = g.GamePrice.IsDiscount,
                Discount = g.GamePrice.Discount,
                PriceDiscount = g.GamePrice.DiscountPrice,
                Price = g.GamePrice.Price
            })
                .ToList();
        }

        //[HttpGet("{countPag}/{tags}/{gameMode}/{features}/{platform}")]
        //public IEnumerable<CatalogViewModel> Get(int countPag, string tags,
        //    string gameMode, string features, string platform)
        //{
        //    var game = _db.Games
        //        .Include(price => price.GamePrice)
        //        .Include(img => img.GameImage)
        //        .ToList();

        //    return game.Select(g => new CatalogViewModel()
        //        {
        //            Id = g.Id,
        //            Image = g.GameImage.ImageInCatalog,
        //            Name = g.Name,
        //            KeyActivate = g.WhereKeyActivated,
        //            Genre = g.Genre,
        //            IsDiscount = g.GamePrice.IsDiscount,
        //            Discount = g.GamePrice.Discount,
        //            PriceDiscount = g.GamePrice.DiscountPrice,
        //            Price = g.GamePrice.Price
        //        })
        //        .Where(item => item.Genre == tags).ToList();
        //}
    }
}
