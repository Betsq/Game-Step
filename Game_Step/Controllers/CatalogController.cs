using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.CatalogController
{
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public CatalogController(ApplicationContext db)
        {
            _db = db;
        }

        [Route("api/catalog/{countPag:int}")]
        [HttpGet]
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

        //[Route("api/catalog/filter/{tags?}/{gameMode?}/{features?}/{platform?}")]
        //[HttpGet]
        //public IEnumerable<CatalogViewModel> GetFilter(string tags = null,
        //    string gameMode = null, string features = null, string platform = null)
        //{
        //    var game = _db.Games
        //        .Include(price => price.GamePrice)
        //        .Include(img => img.GameImage)
        //        .ToList();

        //    return game.Select(g => new CatalogViewModel()
        //    {
        //        Id = g.Id,
        //        Image = g.GameImage.ImageInCatalog,
        //        Name = g.Name,
        //        KeyActivate = g.WhereKeyActivated,
        //        Genre = g.Genre,
        //        IsDiscount = g.GamePrice.IsDiscount,
        //        Discount = g.GamePrice.Discount,
        //        PriceDiscount = g.GamePrice.DiscountPrice,
        //        Price = g.GamePrice.Price
        //    })
        //        .Where(item => item.Genre == tags).ToList();
        //}
    }
}
