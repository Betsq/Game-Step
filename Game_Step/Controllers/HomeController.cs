using Game_Step.Models;
using Game_Step.Util;
using Game_Step.ViewComponent;
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

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            if (HttpContext.Session.Keys.Contains("CartId"))
            {
                bool isHaveId = false;

                var listId = HttpContext.Session.Get<List<int>>("CartId");

                foreach (var lsId in listId)
                {
                    if (lsId == id)
                    {
                        isHaveId = true;

                    }
                }

                if (isHaveId == false)
                {
                    listId.Add(id);
                    HttpContext.Session.Set<List<int>>("CartId", listId);
                }
            }
            else
            {
                List<int> listId = new List<int>();
                listId.Add(id);

                HttpContext.Session.Set<List<int>>("CartId", listId);
            }
            return ViewComponent("Cart", id);
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
