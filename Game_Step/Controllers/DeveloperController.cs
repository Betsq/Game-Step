using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var develop = await db.Developers.ToListAsync();
            return View(develop);
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

        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id != null)
            {
               var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
                if (developer != null)
                {
                    return View(developer);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
                if (developer != null)
                {
                    return View(developer);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Developer developer)
        {
            db.Developers.Update(developer);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
                if (developer != null)
                {
                    return View(developer);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
                if (developer != null)
                {
                    db.Developers.Remove(developer);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();   
        }
    }
}
