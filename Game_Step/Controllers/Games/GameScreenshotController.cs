using Game_Step.Models;
using Game_Step.Models.GamesModel;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers.AllGameController
{
    public class GameScreenshotController : Controller
    {
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment appEnvironment;

        public GameScreenshotController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            this.appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var game = await db.Games
                .Include(gameScreen => gameScreen.GameScreenshots)
                .Include(img => img.GameImage)
                .FirstOrDefaultAsync(image => image.Id == id);

            if (game == null)
                return NotFound();

            var screenshotsViewModel = new GamesScreenshotViewModel()
            {
                GameId = game.Id,
                Name = game.Name,
                PathImage = game.GameImage.InnerImage,
                GameScreenshotsList = game.GameScreenshots,
            };

            return View(screenshotsViewModel);
        }


        [HttpGet]
        public async Task<JsonResult> Delete(int? id)
        {
            if (id == null)
                return Json(false);

            var gameScreenshots = await db.GameScreenshots
                .FirstOrDefaultAsync(image => image.Id == id);

            if (gameScreenshots == null)
                return Json(false);

            string pathToFile = appEnvironment.WebRootPath + gameScreenshots.Screenshot;

            var fileImg = new FileInfo(pathToFile);
            if (fileImg.Exists)
                fileImg.Delete();

            db.GameScreenshots.Remove(gameScreenshots);
            await db.SaveChangesAsync();

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> AddScreenshot(GamesScreenshotViewModel model)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var game = await db.Games.FirstOrDefaultAsync(item => item.Id == model.GameId);

            if (game == null)
                return NotFound();

            string folderAllGames = "/img/Game/Games/" + game.Name + "/";
            int countScreenshots = 1;

            if (model.Screenshots == null)
                return RedirectToAction("Update", "GameScreenshot", new { id = model.GameId });

            foreach (var screenshot in model.Screenshots)
            {
                bool isHaveScreenshots = true;
                while (isHaveScreenshots)
                {
                    string pathToScreenshot = folderAllGames + "Screenshot" + countScreenshots.ToString() + ".jpg";
                    FileInfo fileImg = new FileInfo(appEnvironment.WebRootPath + pathToScreenshot);

                    if (!fileImg.Exists)
                    {
                        countScreenshots++;
                        isHaveScreenshots = false;
                        using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathToScreenshot, FileMode.Create))
                        {
                            await screenshot.CopyToAsync(filesStream);
                        }

                        var gameScreenshot = new GameScreenshot
                        {
                            Screenshot = pathToScreenshot,
                            Game = game,
                        };

                        db.GameScreenshots.Add(gameScreenshot);
                    }
                    else
                    {
                        countScreenshots++;
                    }
                }
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Update", "GameScreenshot", new {id =  model.GameId});
        }

        [HttpGet]
        public IActionResult CallScreenshotPopup() => ViewComponent("Screenshot");
    }
}
