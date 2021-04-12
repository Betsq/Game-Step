using Game_Step.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class GameKeyController : Controller
    {
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _appEnvironment;

        public GameKeyController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddKey()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddKey(GameKey gameKey, IFormFile uploadFile)
        {
            if (uploadFile != null)
            {
                //Uploading the file to the server
                string path = uploadFile.FileName;
                using (var fs = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fs);

                    fs.Close();
                }

                //Reading the file
                using (StreamReader fs = new StreamReader(_appEnvironment.WebRootPath + path))
                {
                    //Looking for a game for which the keys were intended
                    var game = db.Games.FirstOrDefault(item => item.Id == (int)gameKey.GameId);

                    while (true)
                    {
                        //Read the file line by line
                        string s = await fs.ReadLineAsync();

                        //If the lines run out, break the while
                        if (s == null)
                            break;

                        //Creating the model
                        var gk = new GameKey()
                        {
                            Game = game,
                            KeyToGame = s,
                        };

                        await db.GameKeys.AddAsync(gk);
                        await db.SaveChangesAsync();

                    }
                    fs.Close();
                }

                //Check for the presence of a file, if there is one, delete it
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Read(int? id)
        //{
        //    if (id != null)
        //    {
        //        var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
        //        if (developer != null)
        //        {
        //            return View(developer);
        //        }
        //    }
        //    return NotFound();
        //}

        //[HttpGet]
        //public async Task<IActionResult> Update(int? id)
        //{
        //    if (id != null)
        //    {
        //        var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
        //        if (developer != null)
        //        {
        //            return View(developer);
        //        }
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(Developer developer)
        //{
        //    db.Developers.Update(developer);
        //    await db.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //[ActionName("Delete")]
        //public async Task<IActionResult> ConfirmDelete(int? id)
        //{
        //    if (id != null)
        //    {
        //        var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
        //        if (developer != null)
        //        {
        //            return View(developer);
        //        }
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id != null)
        //    {
        //        var developer = await db.Developers.FirstOrDefaultAsync(item => item.Id == id);
        //        if (developer != null)
        //        {
        //            db.Developers.Remove(developer);
        //            await db.SaveChangesAsync();

        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return NotFound();
        //}
    }
}
