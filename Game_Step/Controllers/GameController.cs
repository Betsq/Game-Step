using Game_Step.Models;
using Game_Step.Models.GamesModel;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Game_Step.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationContext db;

        public GameController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await db.Games.ToListAsync();


            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GamesViewModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (model.Image != null)
                {
                    using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Image.Length);
                    }
                }

                Game game = new Game
                {
                    Name = model.Name,
                    Description = model.Description,
                    Image = imageData,
                    Genre = model.Genre,
                    Language = model.Language,
                    QuantityOfGoods = model.QuantityOfGoods,
                    ReleaseDate = model.ReleaseDate,
                    Publisher = model.Publisher,
                    Developer = model.Developer,
                    Features = model.Features,
                    Region = model.Region,
                    WhereKeyActivated = model.WhereKeyActivated,

                    RecommendOC = model.RecommendOC,
                    RecommendCPU = model.RecommendCPU,
                    RecommendRAM = model.RecommendRAM,
                    RecommendVideoCard = model.RecommendVideoCard,
                    RecommendDirectX = model.RecommendDirectX,
                    RecommendHDD = model.RecommendHDD,
                    MinimumOC = model.MinimumOC,
                    MinimumCPU = model.MinimumCPU,
                    MinimumRAM = model.MinimumRAM,
                    MinimumVideoCard = model.MinimumVideoCard,
                    MinimumDirectX = model.MinimumDirectX,
                    MinimumHDD = model.MinimumHDD
                };

                int disc = model.Discount;
                if (disc < 0 || disc > 99 || model.IsDiscount == false)
                {
                    disc = 0;
                    model.IsDiscount = false;
                }
                    
                int discountPrice = 0;
                if (model.IsDiscount == true)
                {
                    discountPrice = model.Price - ((model.Price * model.Discount)/100);
                }

                GamePrice gamePrice = new GamePrice
                {
                    Price = model.Price,
                    IsDiscount = model.IsDiscount,
                    Discount = disc,
                    DiscountPrice = discountPrice,
                    Game = game
                };


                await db.GamePrices.AddAsync(gamePrice);
                await db.SaveChangesAsync();


                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id != null)
            {
                var game = await db.Games.FirstOrDefaultAsync(item => item.Id == id);
                if (game != null)
                {
                    return View(game);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var game = await db.Games.FirstOrDefaultAsync(item => item.Id == id);
                var priceGame = await db.GamePrices.FirstOrDefaultAsync(item => item.GameId == id);
                if (game != null)
                {
                    GamesViewModel gamesViewModel = new GamesViewModel
                    {
                        Id = game.Id,
                        Name = game.Name,
                        Price = priceGame.Price,
                        IsDiscount = priceGame.IsDiscount,
                        Discount = priceGame.Discount,
                        Description = game.Description,
                        Genre = game.Genre,
                        Language = game.Language,
                        QuantityOfGoods = game.QuantityOfGoods,
                        ReleaseDate = game.ReleaseDate,
                        Publisher = game.Publisher,
                        Developer = game.Developer,
                        Features = game.Features,
                        Region = game.Region,
                        WhereKeyActivated = game.WhereKeyActivated,

                        RecommendOC = game.RecommendOC,
                        RecommendCPU = game.RecommendCPU,
                        RecommendRAM = game.RecommendRAM,
                        RecommendVideoCard = game.RecommendVideoCard,
                        RecommendDirectX = game.RecommendDirectX,
                        RecommendHDD = game.RecommendHDD,
                        MinimumOC = game.MinimumOC,
                        MinimumCPU = game.MinimumCPU,
                        MinimumRAM = game.MinimumRAM,
                        MinimumVideoCard = game.MinimumVideoCard,
                        MinimumDirectX = game.MinimumDirectX,
                        MinimumHDD = game.MinimumHDD
                    };
                    return View(gamesViewModel);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(GamesViewModel model)
        {
            Game game = await db.Games.FirstOrDefaultAsync(item => item.Id == model.Id);
            var priceGame = await db.GamePrices.FirstOrDefaultAsync(item => item.GameId == model.Id);
            if (model.Image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Image.Length);
                }
                game.Image = imageData;
            }

            if (game != null)
            {
                game.Id = model.Id;
                game.Name = model.Name;
                game.Description = model.Description;
                game.Genre = model.Genre;
                game.Language = model.Language;
                game.QuantityOfGoods = model.QuantityOfGoods;
                game.ReleaseDate = model.ReleaseDate;
                game.Publisher = model.Publisher;
                game.Developer = model.Developer;
                game.Features = model.Features;
                game.Region = model.Region;
                game.WhereKeyActivated = model.WhereKeyActivated;

                game.RecommendOC = model.RecommendOC;
                game.RecommendCPU = model.RecommendCPU;
                game.RecommendRAM = model.RecommendRAM;
                game.RecommendVideoCard = model.RecommendVideoCard;
                game.RecommendDirectX = model.RecommendDirectX;
                game.RecommendHDD = model.RecommendHDD;
                game.MinimumOC = model.MinimumOC;
                game.MinimumCPU = model.MinimumCPU;
                game.MinimumRAM = model.MinimumRAM;
                game.MinimumVideoCard = model.MinimumVideoCard;
                game.MinimumDirectX = model.MinimumDirectX;
                game.MinimumHDD = model.MinimumHDD;
            };

            int disc = model.Discount;
            if (disc < 0 || disc > 99 || model.IsDiscount == false)
            {
                disc = 0;
                model.IsDiscount = false;
            }

            int discountPrice = 0;
            if (model.IsDiscount == true)
            {
                discountPrice = model.Price - ((model.Price * model.Discount) / 100);
            }

            if (priceGame != null)
            {
                priceGame.Price = model.Price;
                priceGame.IsDiscount = model.IsDiscount;
                priceGame.Discount = model.Discount;
                priceGame.DiscountPrice = discountPrice;
                priceGame.Game = game;
            }

            db.GamePrices.Update(priceGame);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var game = await db.Games.FirstOrDefaultAsync(item => item.Id == id);
                if (game != null)
                {
                    return View(game);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var game = await db.Games.FirstOrDefaultAsync(item => item.Id == id);
            if (game != null)
            {
                db.Games.Remove(game);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Game(int? id)
        {
            if (id != null)
            {
                var game = await db.Games.Include(price => price.GamePrices).FirstOrDefaultAsync(item => item.Id == id);
                if (game != null)
                    return View(game);
            }
            return NotFound();
        }
    }
}
