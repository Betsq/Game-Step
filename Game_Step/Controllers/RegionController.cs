using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class RegionController : Controller
    {
        private readonly ApplicationContext db;

        public RegionController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var region = await db.Regions.ToListAsync();
            return View(region);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Region region)
        {
            await db.Regions.AddAsync(region);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id != null)
            {
                var region = await db.Regions.FirstOrDefaultAsync(item => item.Id == id);
                if (region != null)
                {
                    return View(region);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var region = await db.Regions.FirstOrDefaultAsync(item => item.Id == id);
                if (region != null)
                {
                    return View(region);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Region region)
        {
            db.Regions.Update(region);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var region = await db.Regions.FirstOrDefaultAsync(item => item.Id == id);
                if (region != null)
                {
                    return View(region);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var region = await db.Regions.FirstOrDefaultAsync(item => item.Id == id);
                if (region != null)
                {
                    db.Regions.Remove(region);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
