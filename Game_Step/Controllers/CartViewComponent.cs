using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Game_Step.ViewComponent
{

    public class CartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public CartViewComponent()
        {

        }

        public IViewComponentResult Invoke(Game game)
        {
            List<Game> ls = new List<Game>
            {
               
            };
            ls.Add(game);

            return View(ls);
        }
    }
}
