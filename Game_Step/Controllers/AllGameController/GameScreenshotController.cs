using Game_Step.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

                if (gameScreenshot != null)
                {
                    return View(gameScreenshot);
                }
            }
            return NotFound();
        }
    }
}
