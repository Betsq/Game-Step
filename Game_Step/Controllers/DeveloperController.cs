using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly ApplicationContext db;

        public DeveloperController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Developer developer)
        {
            await db.Developers.AddAsync(developer);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
