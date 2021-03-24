using Game_Step.Models;
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
        public async Task<IActionResult> Create(GamesViewModel gamesViewModel)
        {
            byte[] imageData = null;
            if (gamesViewModel.Image != null)
            {
                using (var binaryReader = new BinaryReader(gamesViewModel.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)gamesViewModel.Image.Length);
                }
            }

            Game game = new Game
            {
                Name = gamesViewModel.Name,
                Price = gamesViewModel.Price,
                Description = gamesViewModel.Description,
                Image = imageData,
                Genre = gamesViewModel.Genre,
                Language = gamesViewModel.Language,
                QuantityOfGoods = gamesViewModel.QuantityOfGoods,
                ReleaseDate = gamesViewModel.ReleaseDate,
                Publisher = gamesViewModel.Publisher,
                Developer = gamesViewModel.Developer,
                Features = gamesViewModel.Features,
                Region = gamesViewModel.Region,
                WhereKeyActivated = gamesViewModel.WhereKeyActivated,

                RecommendOC = gamesViewModel.RecommendOC,
                RecommendCPU = gamesViewModel.RecommendCPU,
                RecommendRAM = gamesViewModel.RecommendRAM,
                RecommendVideoCard = gamesViewModel.RecommendVideoCard,
                RecommendDirectX = gamesViewModel.RecommendDirectX,
                RecommendHDD = gamesViewModel.RecommendHDD,
                MinimumOC = gamesViewModel.MinimumOC,
                MinimumCPU = gamesViewModel.MinimumCPU,
                MinimumRAM = gamesViewModel.MinimumRAM,
                MinimumVideoCard = gamesViewModel.MinimumVideoCard,
                MinimumDirectX = gamesViewModel.MinimumDirectX,
                MinimumHDD = gamesViewModel.MinimumHDD
            };

            await db.Games.AddAsync(game);
            await db.SaveChangesAsync();


            return RedirectToAction("Index");
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
                if (game != null)
                {
                    GamesViewModel gamesViewModel = new GamesViewModel
                    {
                        Id = game.Id,
                        Name = game.Name,
                        Price = game.Price,
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
                game.Price = gamesViewModel.Price;
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
                var game = await db.Games.FirstOrDefaultAsync(item => item.Id == id);
                if (game != null)
                    return View(game);
            }
            return NotFound();
        }
    }
}
