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

        [HttpGet]
        public IEnumerable<CatalogViewModel> Get()
        {

            var gameList = new List<CatalogViewModel>();

            var game =_db.Games
                .Include(price => price.GamePrice)
                .Include(img => img.GameImage)
                .ToList();

            foreach (var g in game)
            {
                var ga = new CatalogViewModel()
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
                };
                gameList.Add(ga);
            }

            return gameList;
        }
    }
}
