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
        private readonly ApplicationContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public GameKeyController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _db = context;
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
            if (uploadFile == null)
                return RedirectToAction("Index");

            //Uploading the file to the server
            string path = uploadFile.FileName;
            using (var fs = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadFile.CopyToAsync(fs);

                fs.Close();
            }

            using (var fs = new StreamReader(_appEnvironment.WebRootPath + path))
            {
                var game = _db.Games.FirstOrDefault(item => item.Id == gameKey.GameId);

                if (game == null)
                    return NotFound();

                while (true)
                {
                    string s = await fs.ReadLineAsync();

                    if (s == null)
                        break;

                    //Creating the model
                    var gk = new GameKey()
                    {
                        Game = game,
                        KeyToGame = s,
                    };

                    await _db.GameKeys.AddAsync(gk);
                    await _db.SaveChangesAsync();

                }
                fs.Close();
            }

            //Check for the presence of a file, if there is one, delete it
            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
            if (fi.Exists)
            {
                fi.Delete();
            }

            return RedirectToAction("Index");
        }
    }
}
