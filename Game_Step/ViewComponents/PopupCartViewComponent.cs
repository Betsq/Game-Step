using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Game_Step.ViewComponent
{

    public class PopupCartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ApplicationContext db;
        public PopupCartViewComponent(ApplicationContext context)
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
                    var priceGame = db.GamePrices.FirstOrDefault(item => item.GameId == id);
                    if (game != null)
                    {
                        Cart cart = new Cart
                        {
                            Id = game.Id,
                            Name = game.Name,
                            Price = priceGame.Price,
                            Quantity = game.QuantityOfGoods,
                            IsDiscount = priceGame.IsDiscount,
                            Discount = priceGame.Discount,
                            DiscountPrice = priceGame.DiscountPrice,
                        };
                        carts.Add(cart);
                    }
                }
            }

            return View(carts);
        }
    }
}
