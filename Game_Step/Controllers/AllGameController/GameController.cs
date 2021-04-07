using Game_Step.Models;
using Game_Step.Models.GamesModel;
using Game_Step.ViewModels;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment appEnvironment;

        public GameController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            this.appEnvironment = appEnvironment;
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
        public async Task<IActionResult> Create(GamesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                int disc = model.Discount;
                int discountPrice = 0;
                if (disc < 0 || disc > 99 || model.IsDiscount == false)
                {
                    disc = 0;
                    model.IsDiscount = false;
                }
                else
                    discountPrice = model.Price - ((model.Price * model.Discount) / 100);


                Game game = new Game
                {
                    Name = model.Name,
                    Description = model.Description,
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

                string nameFolderGame = model.Name + "/";
                string folderAllGames = "/img/Game/Games/";

                Directory.CreateDirectory(appEnvironment.WebRootPath + folderAllGames + nameFolderGame);

                if (model.MainImage != null)
                {
                    string pathMainImage = folderAllGames + nameFolderGame + "Main_Image.jpg";
                    string pathInnerImage = folderAllGames + nameFolderGame + "Inner_Image.jpg";
                    string pathImageInCatalog = folderAllGames + nameFolderGame + "Image_In_Catalog.jpg";

                    using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathMainImage, FileMode.Create))
                        await model.MainImage.CopyToAsync(filesStream);

                    if (model.InnerImage != null)
                    {
                        using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathInnerImage, FileMode.Create))
                            await model.InnerImage.CopyToAsync(filesStream);
                    }

                    if (model.ImageInCatalog != null)
                    {
                        using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathImageInCatalog, FileMode.Create))
                            await model.ImageInCatalog.CopyToAsync(filesStream);
                    }

                    GameImage gameImage = new GameImage
                    {
                        MainImage = pathMainImage,
                        InnerImage = pathInnerImage,
                        ImageInCatalog = pathImageInCatalog,
                        Game = game,
                    };

                    db.GameImages.Add(gameImage);
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
                var imageGame = await db.GameImages.FirstOrDefaultAsync(item => item.GameId == id);

                if (game != null)
                {
                    GamesUpdateViewModel gamesViewModel = new GamesUpdateViewModel
                    {
                        Id = game.Id,
                        Name = game.Name,
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

                        Price = priceGame.Price,
                        IsDiscount = priceGame.IsDiscount,
                        Discount = priceGame.Discount,

                        MainImagePath = imageGame?.MainImage,
                        InnerImagePath = imageGame?.InnerImage,
                        ImageInCatalogPath = imageGame?.ImageInCatalog,

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
        public async Task<IActionResult> Update(GamesUpdateViewModel model)
        {
            var game = await db.Games.FirstOrDefaultAsync(item => item.Id == model.Id);
            var priceGame = await db.GamePrices.FirstOrDefaultAsync(item => item.GameId == model.Id);
            var imageGame = await db.GameImages.FirstOrDefaultAsync(item => item.GameId == model.Id);

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
            bool isDesc = model.IsDiscount;
            int discountPrice = 0;
            if (disc <= 0 || disc > 99 || isDesc == false)
            {
                disc = 0;
                isDesc = false;
            }
            else
                discountPrice = model.Price - ((model.Price * model.Discount) / 100);


            if (priceGame != null)
            {
                priceGame.Price = model.Price;
                priceGame.IsDiscount = isDesc;
                priceGame.Discount = disc;
                priceGame.DiscountPrice = discountPrice;
                priceGame.Game = game;
            }


            string nameFolderGame = model.Name + "/";
            string folderAllGames = "/img/Game/Games/";

            Directory.CreateDirectory(appEnvironment.WebRootPath + folderAllGames + nameFolderGame);

            string pathMainImage = folderAllGames + nameFolderGame + "Main_Image.jpg";
            string pathInnerImage = folderAllGames + nameFolderGame + "Inner_Image.jpg";
            string pathImageInCatalog = folderAllGames + nameFolderGame + "Image_In_Catalog.jpg";

            if (model.MainImage != null)
            {
                using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathMainImage, FileMode.Create))
                    await model.MainImage.CopyToAsync(filesStream);
            }

            if (model.InnerImage != null)
            {
                using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathInnerImage, FileMode.Create))
                    await model.InnerImage.CopyToAsync(filesStream);
            }

            if (model.ImageInCatalog != null)
            {
                using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathImageInCatalog, FileMode.Create))
                    await model.ImageInCatalog.CopyToAsync(filesStream);
            }

            if (imageGame != null)
            {
                imageGame.MainImage = pathMainImage;
                imageGame.InnerImage = pathInnerImage;
                imageGame.ImageInCatalog = pathImageInCatalog;
                imageGame.Game = game;
            }

            db.GameImages.Update(imageGame);
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
                string folderGame = appEnvironment.WebRootPath + "/img/Game/Games/" + game.Name;
                if (Directory.Exists(folderGame))
                {
                    Directory.Delete(folderGame, true);
                }
 
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
                var game = await db.Games.Include(price => price.GamePrice)
                    .Include(image => image.GameImage)
                    .Include(screenshot => screenshot.GameScreenshots)
                    .FirstOrDefaultAsync(item => item.Id == id);
                if (game != null)
                {
                    GameViewModel model = new GameViewModel
                    {
                        Game = game,
                    };
                    return View(model);
                }
                    
            }
            return NotFound();
        }
    }
}
