using Game_Step.Models;
using Game_Step.ViewComponent;
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

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var game = await db.Games.FirstOrDefaultAsync(item => item.Id == id);

            return ViewComponent("Cart", game);
        }

        public IActionResult ControlPanel()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
