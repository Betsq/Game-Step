using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Game_Step.Models;
using Game_Step.Models.GamesModel;
using Game_Step.ViewModels;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.Controllers.Games
{
    public class GameController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;
        public GameController(ApplicationContext context,
            IWebHostEnvironment appEnvironment, UserManager<User> userManager)
        {
            _db = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _db.Games.ToListAsync());

        [HttpGet]
        public IActionResult Create()
        {
            var tags = _db.Tags.ToList();

            var gameCreate = new GamesCreateViewModel()
            {
                Tags = tags,
            };
            return View(gameCreate);
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

            await AddGameTags(game, model.TagsDictionary);
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
                    .Include(item => item.GameTags)
                    .FirstOrDefaultAsync(item => item.Id == id);

            if (game == null)
                return NotFound();

            var tags = await _db.Tags.ToListAsync();

            var gamesViewModel = new GamesUpdateViewModel
            {
                Game = game,
                Price = game.GamePrice,
                GameRecommendation = game.Recommendation,
                GameMinimum = game.Minimum,
                Tags = tags,
                Id = game.Id,

                MainImagePath = game.GameImage.MainImage,
                InnerImagePath = game.GameImage.InnerImage,
                ImageInCatalogPath = game.GameImage.ImageInCatalog,
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
                .Include(item => item.GameTags)
                .FirstOrDefaultAsync(item => item.Id == model.Id);

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

            DelGameTags(game, model.TagsDictionary);
            await AddGameTags(game, model.TagsDictionary);
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

            if (image == null)
            {
                image = new GameImage
                {
                    MainImage = pathMainImage,
                    InnerImage = pathInnerImage,
                    ImageInCatalog = pathImageInCatalog,
                    Game = game,
                };
                return (image);
            }

            image.MainImage = pathMainImage;
            image.InnerImage = pathInnerImage;
            image.ImageInCatalog = pathImageInCatalog;
            image.Game = game;

            return (image);
        }

        public GamePrice PriceCalculation(Game game, GamePrice gamePrice)
        {
            int percentDiscount = gamePrice.Discount;
            bool isDiscount = gamePrice.IsDiscount;
            int discountPrice = 0;

            if (percentDiscount is <= 0 or > 99 || isDiscount == false)
            {
                percentDiscount = 0;
                isDiscount = false;
            }
            else
                discountPrice = gamePrice.Price - ((gamePrice.Price * gamePrice.Discount) / 100);

            GamePrice gp = game.GamePrice;

            if (gp == null)
            {
                gp = new GamePrice()
                {
                    IsDiscount = isDiscount,
                    Discount = percentDiscount,
                    DiscountPrice = discountPrice,
                    Price = gamePrice.Price,
                    Game = game,
                };
                return (gp);
            }

            gp.IsDiscount = isDiscount;
            gp.Discount = percentDiscount;
            gp.DiscountPrice = discountPrice;
            gp.Price = gamePrice.Price;
            gp.Game = game;
            return (gp);
        }

        public async Task AddGameTags(Game game, Dictionary<int, GamesTagsViewModel> tagsDictionary)
        {
            foreach (var td in tagsDictionary)
            {
                if (td.Value.IsAdd == false)
                    continue;

                var gameTag = game.GameTags?.FirstOrDefault(item => item.TagId == td.Key);

                if (gameTag != null)
                    continue;

                var tg = await _db.Tags
                    .FirstOrDefaultAsync(item => item.Id == td.Key);
                if (tg == null)
                    continue;

                var gt = new GameTags
                {
                    Show = td.Value.Show,
                    Tag = tg,
                    Game = game
                };
                _db.GameTags.Add(gt);
            }
        }

        public void DelGameTags(Game game, Dictionary<int, GamesTagsViewModel> tagsDictionary)
        {
            var gameTags = game.GameTags?.ToList();

            if (gameTags == null)
                return;

            foreach (var td in tagsDictionary)
            {
                var gameTag = gameTags
                        .FirstOrDefault(item => item.TagId == td.Key);

                if (gameTag == null)
                    continue;

                if (td.Value.IsAdd == true)
                    gameTags.Remove(gameTag);

            }

            _db.GameTags.RemoveRange(gameTags);
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
            if (id == null)
                return RedirectToAction("Index");

            var game = _db.Games.FirstOrDefault(item => item.Id == id);

            if (game == null)
                return RedirectToAction("Index");

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
                .Include(min => min.Minimum)
                .Include(rec => rec.Recommendation)
                .Include(screenshots => screenshots.GameScreenshots)
                .Include(comment => comment.MainComments)
                    .ThenInclude(subComment => subComment.SubComments)
                        .ThenInclude(item => item.User)
                .Include(comment => comment.MainComments)
                    .ThenInclude(item => item.User)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (game == null)
                return NotFound();

            var user = _userManager.GetUserAsync(HttpContext.User);

            var model = new GameViewModel
            {
                Game = game,
                Name = user?.Result?.Name,
                Avatar = user?.Result?.Avatar,
            };

            return View(model);
        }
    }
}
