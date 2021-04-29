using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;

namespace Game_Step.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationContext _db;
        public TagController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tags = _db.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag model)
        {
            _db.Tags.Add(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
                return NotFound();

            var tag = _db.Tags.Find(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost]
        public IActionResult Update(Tag model)
        {
            _db.Tags.Update(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var tag = _db.Tags.Find(id);

            if (tag == null)
                return RedirectToAction("Index");

            _db.Tags.Remove(tag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
