using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationContext db;

        public PublisherController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
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
        public async Task<IActionResult> Create(Publisher publisher)
        {
            await db.Publishers.AddAsync(publisher);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id != null)
            {
               var publisher = await db.Publishers.FirstOrDefaultAsync(item => item.Id == id);
                if (publisher != null)
                {
                    return View(publisher);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var publisher = await db.Publishers.FirstOrDefaultAsync(item => item.Id == 1);
                if (publisher != null)
                {
                    return View(publisher);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Publisher publisher)
        {
            db.Publishers.Update(publisher);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var publisher = await db.Publishers.FirstOrDefaultAsync(item => item.Id == id);
                if (publisher != null)
                {
                    return View(publisher);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var publisher = await db.Publishers.FirstOrDefaultAsync(item => item.Id == id);
                if (publisher != null)
                {
                    db.Publishers.Remove(publisher);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
