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
            if (id != null)
            {
                var gameScreenshot = await db.GameScreenshots.Where(item => item.GameId == id)
                    .OrderBy(item => item.Screenshot).ToListAsync();
                var gameImage = await db.GameImages.FirstOrDefaultAsync(image => image.GameId == id);
                var game = await db.Games.FirstOrDefaultAsync(image => image.Id == id);

                if (gameImage != null && game != null)
                {
                    var screenshotViewModel = new GamesScreenshotViewModel()
                    {
                        GameId = game.Id,
                        Name = game.Name,
                        PathImage = gameImage.InnerImage,
                        GameScreenshotsList = gameScreenshot,
                    };
                    return View(screenshotViewModel);
                }
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<JsonResult> Delete(int? id)
        {
            if (id != null)
            {
                var gameScreenshot = await db.GameScreenshots.FirstOrDefaultAsync(image => image.Id == id);
                if (gameScreenshot != null)
                {
                    string pathToFile = appEnvironment.WebRootPath + gameScreenshot.Screenshot;
                    FileInfo fileImg = new FileInfo(pathToFile);
                    if (fileImg.Exists)
                    {
                        fileImg.Delete();
                    }

                    db.GameScreenshots.Remove(gameScreenshot);
                    await db.SaveChangesAsync();

                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> AddScreenshot(GamesScreenshotViewModel model)
        {
            if (ModelState.IsValid)
            {
                var game = await db.Games.FirstOrDefaultAsync(item => item.Id == model.GameId);

                if (game != null)
                {
                    string folderAllGames = "/img/Game/Games/" + game.Name + "/";
                    int countScreenshot = 1;

                    if (model.Screenshots != null)
                    {
                        foreach (var screenshot in model.Screenshots)
                        {

                            bool isHaveScreenshot = true;
                            while (isHaveScreenshot)
                            {
                                string pathToScreenshot = folderAllGames + "Screenshot" + countScreenshot.ToString() + ".jpg";
                                FileInfo fileImg = new FileInfo(appEnvironment.WebRootPath + pathToScreenshot);
                                if (!fileImg.Exists)
                                {
                                    countScreenshot++;
                                    isHaveScreenshot = false;
                                    using (var filesStream = new FileStream(appEnvironment.WebRootPath + pathToScreenshot, FileMode.Create))
                                    {
                                        await screenshot.CopyToAsync(filesStream);
                                    }

                                    GameScreenshot gameScreenshot = new GameScreenshot
                                    {
                                        Screenshot = pathToScreenshot,
                                        Game = game,
                                    };
                                    db.GameScreenshots.Add(gameScreenshot);

                                }
                                else
                                {
                                    countScreenshot++;
                                }
                            }
                        }
                        await db.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("Update", "GameScreenshot", new {id =  model.GameId});
        }

        [HttpGet]
        public IActionResult CallScreenshotPopup()
        {
            return ViewComponent("Screenshot");
        }
    }
}
