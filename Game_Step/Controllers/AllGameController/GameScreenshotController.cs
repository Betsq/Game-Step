using Game_Step.Models;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                var gameScreenshot = await db.GameScreenshots.OrderByDescending(item => item.GameId == id).ToListAsync();
                var gameImage = await db.GameImages.FirstOrDefaultAsync(image => image.GameId == id);
                var game = await db.Games.FirstOrDefaultAsync(image => image.Id == id);

                if (gameScreenshot != null && gameImage != null && game != null)
                {
                    var screenshotViewModel = new GamesScreenshotViewModel()
                    {
                        Id = game.Id,
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
    }
}
