using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _db = context;
            _logger = logger;
        }



        public IActionResult ControlPanel()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var game = await _db.Games
                .Include(price => price.GamePrice)
                .Include(image => image.GameImage).ToListAsync();

            var category = await _db.Categories.ToListAsync();

            var sliders = await _db.MainItemSliders
                .Include(item => item.Game)
                .Include(item => item.AdditionalItem)
                    .ThenInclude(item => item.Game)
                .ToListAsync();


            var homePage = new HomePageViewModel
            {
                Games = game,
                Categories = category,
                Sliders = sliders
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
