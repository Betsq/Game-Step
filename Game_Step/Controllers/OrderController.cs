using Game_Step.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext db;

        public OrderController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult Order(Dictionary<int, int> items)
        {
            if (items != null)
            {
                var listProdId = HttpContext.Session.Get<List<int>>("CartId");
                if (listProdId.Count() >= 0)
                {
                    bool isOrderValide = false;

                    OrderNumber orderNumber = new OrderNumber { };
                    db.OrderNumbers.Add(orderNumber);
                    foreach (var item in items)
                    {
                        foreach (var prodId in listProdId)
                        {
                            if (item.Key == prodId)
                            {
                                var game = db.Games.Include(price => price.GamePrice).FirstOrDefault(g => g.Id == item.Key);
                                if (game != null)
                                {
                                    isOrderValide = true;

                                    int gamePrice;
                                    if (game.GamePrice.IsDiscount)
                                    {
                                        gamePrice = game.GamePrice.DiscountPrice * item.Value;
                                    }
                                    else
                                    {
                                        gamePrice = game.GamePrice.Price * item.Value;
                                    }

                                    Order order = new Order
                                    {
                                        ProductId = item.Key,
                                        AmountProduct = item.Value,
                                        ProductPrice = gamePrice,
                                        Email = "gmain2@gmail.com",
                                        OrderNumber = orderNumber,
                                    };

                                    db.Orders.Add(order);
                                }
                            }
                        }
                    }
                    if (isOrderValide)
                    {
                        HttpContext.Session.Remove("CartId");
                        HttpContext.Session.Remove("CountOfGoods");
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }   
}
