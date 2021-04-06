using Game_Step.Models;
using Game_Step.Util;
using Game_Step.ViewComponent;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            db = context;
            _logger = logger;
        }



        public IActionResult ControlPanel()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var game = await db.Games.Include(price => price.GamePrice).Include(image => image.GameImage).ToListAsync();
            var ct = await db.Categories.ToListAsync();

            HomePageViewModel homePage = new HomePageViewModel
            {
                Games = game,
                Categories = ct,
            };
            return View(homePage);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
