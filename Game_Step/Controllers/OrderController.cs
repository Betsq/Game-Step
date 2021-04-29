using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game_Step.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _db;

        public OrderController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Order(CartOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Cart", model);

            var listProdId = HttpContext.Session.Get<List<int>>("CartId");
            if (listProdId.Count() < 0)
                return RedirectToAction("Index", "Cart", model);


            bool isOrderValide = false;
            int totalPrice = 0;

            var orderNumber = new OrderNumber
            {
                Email = model.Email,
                UserAgreement = model.UserAgreement,
                PaymentMethod = model.PaymentMethod,
                Promocode = model.Promocode,
                OrderTime = DateTime.Now,
            };

            //Retrives a dictionary
            foreach (var item in model.Items)
            {
                //Get a list with an Id 
                foreach (var prodId in listProdId)
                {
                    //Comparing an Id from a dictionary with a list  
                    if (item.Key != prodId || model.UserAgreement != true)
                        continue;
                    var game = _db.Games.Include(price => price.GamePrice).FirstOrDefault(g => g.Id == item.Key);
                    if (game == null)
                        continue;

                    isOrderValide = true;

                    //Price of the current game
                    int gamePrice;

                    //If there is a discount on the game
                    if (game.GamePrice.IsDiscount)
                    {
                        gamePrice = game.GamePrice.DiscountPrice;

                        totalPrice += game.GamePrice.DiscountPrice * item.Value;
                    }
                    else
                    {
                        gamePrice = game.GamePrice.Price;

                        totalPrice += game.GamePrice.Price * item.Value;
                    }

                    var order = new Order
                    {
                        ProductId = item.Key,
                        AmountProduct = item.Value,
                        ProductPrice = gamePrice,
                        OrderNumber = orderNumber,
                    };

                    _db.Orders.Add(order);

                    break;
                }
            }

            if (!isOrderValide)
                return RedirectToAction("Index", "Cart", model);

            orderNumber.TotalPrice = totalPrice;

            _db.OrderNumbers.Add(orderNumber);
            HttpContext.Session.Remove("CartId");
            HttpContext.Session.Remove("CountOfGoods");
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
