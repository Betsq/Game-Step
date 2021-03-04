using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var games = await db.Games.Include(u => u.MinSysReq).Include(u => u.RecSysReq).ToListAsync();
            

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
            var gm = new Game
            {
                Article = gamesViewModel.Article,
                Name = gamesViewModel.Name,
                Price = gamesViewModel.Price,
                Language = gamesViewModel.Language,
                ReleaseDate = gamesViewModel.ReleaseDate,
                Publisher = gamesViewModel.Publisher,
                Developer = gamesViewModel.Developer,
                Features = gamesViewModel.Features,
                Region = gamesViewModel.Region,
            };

            var recSyRe = new RecommendedSystemRequirements
            {
                Game = gm,
                OC = gamesViewModel.RecOC,
                CPU = gamesViewModel.RecCPU,
                RAM = gamesViewModel.RecRAM,
                VideoCard = gamesViewModel.RecVideoCard,
                DirectX = gamesViewModel.RecDirectX,
                HDD = gamesViewModel.RecHDD,
            };

            var minSyRe = new MinimumSystemRequirements
            {
                Game = gm,
                OC = gamesViewModel.MinOC,
                CPU = gamesViewModel.MinCPU,
                RAM = gamesViewModel.MinRAM,
                VideoCard = gamesViewModel.MinVideoCard,
                DirectX = gamesViewModel.MinDirectX,
                HDD = gamesViewModel.MinHDD,
            };

            
            
            db.MinimumSystemRequirements.Add(minSyRe);
            db.RecommendedSystemRequirements.Add(recSyRe);
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
                    return View(game);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Game game)
        {
            db.Games.Update(game);
            await db.SaveChangesAsync();

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
    }
}
