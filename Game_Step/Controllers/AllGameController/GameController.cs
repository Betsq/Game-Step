using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Game_Step.Models;
using Game_Step.Models.GamesModel;
using Game_Step.ViewModels;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.Controllers.AllGameController
{
    public class GameController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public GameController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _db = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await _db.Games.ToListAsync();

            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GamesCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Game game = new()
            {
                Name = model.Game.Name,
                QuantityOfGoods = model.Game.QuantityOfGoods,
                Description = model.Game.Description,
                Language = model.Game.Language,
                Genre = model.Game.Genre,
                Publisher = model.Game.Publisher,
                Developer = model.Game.Developer,
                Features = model.Game.Features,
                Region = model.Game.Region,
                WhereKeyActivated = model.Game.WhereKeyActivated,
                ReleaseDate = model.Game.ReleaseDate,
            };



            _db.GameImages.Add(AddUpdateGameImage(game, model.MainImage, model.InnerImage, model.ImageInCatalog));
            _db.GamePrices.Add(PriceCalculation(game, model.Price));
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await _db.Games.FirstOrDefaultAsync(item => item.Id == id);
            if (game == null)
                return NotFound();

            return View(game);


        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await _db.Games
                    .Include(item => item.GamePrice)
                    .Include(item => item.GameImage)
                    .Include(item => item.Recommendation)
                    .Include(item => item.Minimum)
                    .FirstOrDefaultAsync(item => item.Id == id);

            if (game == null)
                return NotFound();

            GamesUpdateViewModel gamesViewModel = new GamesUpdateViewModel
            {
                Game = game,
                Price = game.GamePrice,
                GameRecommendation = game.Recommendation,
                GameMinimum = game.Minimum,

                MainImagePath = game.GameImage?.MainImage,
                InnerImagePath = game.GameImage?.InnerImage,
                ImageInCatalogPath = game.GameImage?.ImageInCatalog,
            };

            return View(gamesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GamesUpdateViewModel model)
        {
            var game = await _db.Games
                .Include(item => item.GamePrice)
                .Include(item => item.GameImage)
                .Include(item => item.Recommendation)
                .Include(item => item.Minimum)
                .FirstOrDefaultAsync(item => item.Id == model.Game.Id);

            if (game == null)
                return View(model);

            game.Name = model.Game.Name;
            game.QuantityOfGoods = model.Game.QuantityOfGoods;
            game.Description = model.Game.Description;
            game.Language = model.Game.Language;
            game.Genre = model.Game.Genre;
            game.Publisher = model.Game.Publisher;
            game.Developer = model.Game.Developer;
            game.Features = model.Game.Features;
            game.Region = model.Game.Region;
            game.WhereKeyActivated = model.Game.WhereKeyActivated;
            game.ReleaseDate = model.Game.ReleaseDate;
            game.Recommendation = model.GameRecommendation;
            game.Minimum = model.GameMinimum;

            AddUpdateGameImage(game, model.MainImage, model.InnerImage, model.ImageInCatalog);
            PriceCalculation(game, model.Price);

            _db.Games.Update(game);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public GameImage AddUpdateGameImage(Game game, IFormFile mainImage, IFormFile innerImage, IFormFile imageInCatalog)
        {
            string folderAllGames = "/img/Game/Games/" + game.Name + "/";

            if (!Directory.Exists(_appEnvironment.WebRootPath + folderAllGames))
                Directory.CreateDirectory(_appEnvironment.WebRootPath + folderAllGames);

            string pathMainImage = folderAllGames + "Main_Image.jpg";
            string pathInnerImage = folderAllGames + "Inner_Image.jpg";
            string pathImageInCatalog = folderAllGames + "Image_In_Catalog.jpg";

            if (mainImage != null)
            {
                using var filesStream = new FileStream(_appEnvironment.WebRootPath + pathMainImage, FileMode.Create);
                mainImage.CopyTo(filesStream);
            }
            if (innerImage != null)
            {
                using var filesStream = new FileStream(_appEnvironment.WebRootPath + pathInnerImage, FileMode.Create);
                innerImage.CopyTo(filesStream);
            }
            if (imageInCatalog != null)
            {
                using var filesStream = new FileStream(_appEnvironment.WebRootPath + pathImageInCatalog, FileMode.Create);
                imageInCatalog.CopyTo(filesStream);
            }

            GameImage image = game.GameImage;

            if (image != null)
            {
                image.MainImage = pathMainImage;
                image.InnerImage = pathInnerImage;
                image.ImageInCatalog = pathImageInCatalog;
                image.Game = game;
            }
            else
            {
                image = new GameImage
                {
                    MainImage = pathMainImage,
                    InnerImage = pathInnerImage,
                    ImageInCatalog = pathImageInCatalog,
                    Game = game,
                };
            }
            return (image);
        }

        public GamePrice PriceCalculation(Game game, GamePrice gamePrice)
        {

            int percentDiscount = gamePrice.Discount;
            bool isDesc = gamePrice.IsDiscount;
            int discountPrice = 0;

            if (percentDiscount is <= 0 or > 99 || isDesc == false)
            {
                percentDiscount = 0;
                isDesc = false;
            }
            else
                discountPrice = gamePrice.Price - ((gamePrice.Price * gamePrice.Discount) / 100);

            GamePrice gp = game.GamePrice;

            if (gp != null)
            {
                gp.IsDiscount = isDesc;
                gp.Discount = percentDiscount;
                gp.DiscountPrice = discountPrice;
                gp.Price = gamePrice.Price;
                gp.Game = game;
            }
            else
            {
                gp = new GamePrice()
                {
                    IsDiscount = isDesc,
                    Discount = percentDiscount,
                    DiscountPrice = discountPrice,
                    Price = gamePrice.Price,
                    Game = game,
                };
            }
            return (gp);
        }


        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
                return NotFound();

            var game = _db.Games.FirstOrDefault(item => item.Id == id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var game = await _db.Games.FirstOrDefaultAsync(item => item.Id == id);

            if (game == null)
                return NotFound();

            string folderGame = _appEnvironment.WebRootPath + "/img/Game/Games/" + game.Name;

            if (Directory.Exists(folderGame))
                Directory.Delete(folderGame, true);


            _db.Games.Remove(game);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Game(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await _db.Games.Include(price => price.GamePrice)
                .Include(image => image.GameImage)
                .Include(screenshots => screenshots.GameScreenshots)
                .Include(comment => comment.MainComments)
                .ThenInclude(subComment => subComment.SubComments)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (game == null)
                return NotFound();

            var model = new GameViewModel
            {
                Game = game,
            };
            return View(model);
        }
        public IActionResult Catalog()
        {
            return View();
        }
    }
}
