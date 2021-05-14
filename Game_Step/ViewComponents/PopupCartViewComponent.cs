using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.ViewComponent
{
    public class PopupCartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ApplicationContext _db;
        public PopupCartViewComponent(ApplicationContext context)
        {
            _db = context;
        }

        public IViewComponentResult Invoke()
        {
            var listId = HttpContext.Session.Get<List<int>>("CartId");
            var gameList = new List<Game>();

            if (listId == null)
                return View(gameList);

            foreach (var id in listId)
            {
                var game = _db.Games
                    .Include(item => item.GamePrice)
                    .Include(item => item.GameImage)
                    .FirstOrDefault(item => item.Id == id);

                if (game != null)
                    gameList.Add(game);
            }

            return View(gameList);
        }
    }
}
