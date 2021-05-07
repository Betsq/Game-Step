using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Game_Step.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationContext _db;
        public CartController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listId = HttpContext.Session.Get<List<int>>("CartId");
            var gameList = new List<Game>();

            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var game = _db.Games.Include(price => price.GamePrice)
                        .Include(image => image.GameImage)
                        .FirstOrDefault(item => item.Id == id);

                    if (game != null)
                        gameList.Add(game);
                }
            }

            var cartOrder = new CartOrderViewModel
            {
                GameInCart = gameList,
            };

            return View(cartOrder);
        }

        [HttpGet]
        public IActionResult CallCart() => ViewComponent("PopupCart");

        [HttpPost]
        public JsonResult CountOfGoods()
        {
            //if CountOfGoods is set in the session, then we return json with the number of items in the cart.
            //Otherwise, we return an empty string.
            if (!HttpContext.Session.Keys.Contains("CountOfGoods"))
                return Json(0);

            var countOfGoods = HttpContext.Session.Get<int>("CountOfGoods");

            //If the number of products is 0 or less, then delete the session object
            if (countOfGoods > 0)
                return Json(countOfGoods);

            HttpContext.Session.Remove("CountOfGoods");
            return Json(0);
        }

        [HttpPost]
        public JsonResult IsProsuctInCart(int id)
        {
            if (!HttpContext.Session.Keys.Contains("CartId"))
                return Json(false);

            var listId = HttpContext.Session.Get<List<int>>("CartId");

            //Check if the incoming "id" is already in the list
            //DON'T FORGET TO CHANGE
            foreach (var lsId in listId)
            {
                if (lsId == id)
                    return Json(true);
            }

            return Json(false);
        }

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            bool isHaveIdInList = false;
            int countOfGoods;

            //If the "cart" session is set
            if (HttpContext.Session.Keys.Contains("CartId"))
            {
                //get the list with id
                var listId = HttpContext.Session.Get<List<int>>("CartId");

                //Check if the incoming "id" is already in the list
                //DON'T FORGET TO CHANGE
                foreach (var lsId in listId)
                {
                    if (lsId != id)
                        continue;

                    isHaveIdInList = true;
                    break;
                }

                //if "id" in the list is not found, we add "id" to the list
                if (isHaveIdInList == false)
                {
                    listId.Add(id);
                    HttpContext.Session.Set("CartId", listId);
                }
            }
            //If the "cart" session is not set
            else
            {
                //Create a new list
                var listId = new List<int> {id};

                //Set a session
                HttpContext.Session.Set("CartId", listId);
            }

            if (HttpContext.Session.Keys.Contains("CountOfGoods") && isHaveIdInList == false)
            {
                countOfGoods = HttpContext.Session.Get<int>("CountOfGoods");
                //add the number of items in the cart
                countOfGoods += 1;
                HttpContext.Session.Set("CountOfGoods", countOfGoods);

                return ViewComponent("PopupCart");
            }

            if (isHaveIdInList == false)
                HttpContext.Session.Set("CountOfGoods", 1);

            return ViewComponent("PopupCart");
        }

        [HttpGet]
        public IActionResult DeleteProductInCart(int id)
        {
            if (!HttpContext.Session.Keys.Contains("CartId"))
                return ViewComponent("PopupCart");

            //get the list with id
            var listId = HttpContext.Session.Get<List<int>>("CartId");

            //Check if the passed "id" is in the list, if true, remove "id" from the list
            foreach (var lsId in listId.ToList())
            {
                if (lsId != id)
                    continue;

                listId.Remove(id);
                HttpContext.Session.Set<List<int>>("CartId", listId);

                //Get item quantity
                int countOfGoods = HttpContext.Session.Get<int>("CountOfGoods");
                //Decrease the quantity of goods by 1
                countOfGoods -= 1;
                //Set the session object with a new value
                HttpContext.Session.Set("CountOfGoods", countOfGoods);
            }
            return ViewComponent("PopupCart");
        }

        [HttpPost]
        public JsonResult CountPrice(int? id, int? amountProduct)
        {
            if (id == null)
                return Json("");

            if (amountProduct == null)
                return Json("");

            //If the "cart" session is set
            if (!HttpContext.Session.Keys.Contains("CartId"))
                return Json("");

            //get the list with id
            var listId = HttpContext.Session.Get<List<int>>("CartId");

            //Check if the incoming "id" is already in the list
            //DON'T FORGET TO CHANGE
            foreach (var lsId in listId)
            {
                if (lsId != id)
                    continue;

                var gamePrice = _db.GamePrices.FirstOrDefault(item => item.GameId == id);

                if (gamePrice.IsDiscount)
                    return Json(gamePrice.DiscountPrice * amountProduct);

                return Json(gamePrice.Price * amountProduct);
            }

            return Json("");
        }
    }
}
