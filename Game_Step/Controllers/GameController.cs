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
        public IActionResult Update(GamesViewModel gamesViewModel)
        {
            Game game = db.Games.FirstOrDefault(item => item.Id == gamesViewModel.Id);

            if (gamesViewModel.Image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(gamesViewModel.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)gamesViewModel.Image.Length);
                }
                game.Image = imageData;
            }

            if (game != null)
            {
                game.Id = gamesViewModel.Id;
                game.Name = gamesViewModel.Name;
                game.Description = gamesViewModel.Description;
                game.Genre = gamesViewModel.Genre;
                game.Language = gamesViewModel.Language;
                game.QuantityOfGoods = gamesViewModel.QuantityOfGoods;
                game.ReleaseDate = gamesViewModel.ReleaseDate;
                game.Publisher = gamesViewModel.Publisher;
                game.Developer = gamesViewModel.Developer;
                game.Features = gamesViewModel.Features;
                game.Region = gamesViewModel.Region;
                game.WhereKeyActivated = gamesViewModel.WhereKeyActivated;

                game.RecommendOC = gamesViewModel.RecommendOC;
                game.RecommendCPU = gamesViewModel.RecommendCPU;
                game.RecommendRAM = gamesViewModel.RecommendRAM;
                game.RecommendVideoCard = gamesViewModel.RecommendVideoCard;
                game.RecommendDirectX = gamesViewModel.RecommendDirectX;
                game.RecommendHDD = gamesViewModel.RecommendHDD;
                game.MinimumOC = gamesViewModel.MinimumOC;
                game.MinimumCPU = gamesViewModel.MinimumCPU;
                game.MinimumRAM = gamesViewModel.MinimumRAM;
                game.MinimumVideoCard = gamesViewModel.MinimumVideoCard;
                game.MinimumDirectX = gamesViewModel.MinimumDirectX;
                game.MinimumHDD = gamesViewModel.MinimumHDD;
            };

            
            
            

            db.Games.Update(game);
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
