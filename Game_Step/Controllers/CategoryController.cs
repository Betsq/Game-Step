using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationContext db;

        public CategoryController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = model.Name,
                };

                if (model.Image != null)
                {
                    byte[] imageData = null;

                    using (var binaryFile = new BinaryReader(model.Image.OpenReadStream()))
                    {
                        imageData = binaryFile.ReadBytes((int)model.Image.Length);
                    }

                    category.Image = imageData;
                }

                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var ct = await db.Categories.FirstOrDefaultAsync(item => item.Id == id);

                if (ct != null)
                {
                    return View(ct);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel model)
        {
            var ct = await db.Categories.FirstOrDefaultAsync(item => item.Id == model.Id);

            if (ct != null)
            {
                ct.Name = model.Name;

                if (ct.Name != null)
                {
                    byte[] imageData = null;

                    using (var binaryFile = new BinaryReader(model.Image.OpenReadStream()))
                    {
                        imageData = binaryFile.ReadBytes((int)model.Image.Length);
                    }

                    ct.Image = imageData;
                }

                db.Categories.Update(ct);
                await db.SaveChangesAsync();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id != null)
            {
                var ct = await db.Categories.FirstOrDefaultAsync(item => item.Id == id);
                if (ct != null)
                {
                    db.Categories.Remove(ct);
                    await db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
