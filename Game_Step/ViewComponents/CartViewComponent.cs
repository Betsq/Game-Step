using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Game_Step.ViewComponent
{

    public class CartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ApplicationContext db;
        public CartViewComponent(ApplicationContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var listId = HttpContext.Session.Get<List<int>>("CartId");
            List<Cart> carts = new List<Cart>();
            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var game = db.Games.Find(id);
                    if (game != null)
                    {
                        Cart cart = new Cart
                        {
                            Id = game.Id,
                            Name = game.Name,
                        };
                        carts.Add(cart);
                    }
                }
            }

            return View(carts);
        }
    }
}
