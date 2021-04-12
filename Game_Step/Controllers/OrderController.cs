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
        private readonly ApplicationContext db;

        public OrderController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult Order(CartOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var listProdId = HttpContext.Session.Get<List<int>>("CartId");
                if (listProdId.Count() >= 0)
                {
                    //if one or more product valide, it will be true 
                    bool isOrderValide = false;
                    //The total price of all products 
                    int totalPrice = 0;

                    OrderNumber orderNumber = new OrderNumber
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
                            if (item.Key == prodId && model.UserAgreement == true)
                            {
                                var game = db.Games.Include(price => price.GamePrice).FirstOrDefault(g => g.Id == item.Key);
                                if (game != null)
                                {
                                    isOrderValide = true;

                                    //Price of the current game
                                    int gamePrice;

                                    //If there is a discount on the game
                                    if (game.GamePrice.IsDiscount)
                                    {
                                        gamePrice = game.GamePrice.DiscountPrice;

                                        //Adding the price of the current game to the total price
                                        totalPrice += game.GamePrice.DiscountPrice * item.Value;
                                    }
                                    else
                                    {
                                        gamePrice = game.GamePrice.Price;

                                        totalPrice += game.GamePrice.Price * item.Value;
                                    }

                                    Order order = new Order
                                    {
                                        ProductId = item.Key,
                                        AmountProduct = item.Value,
                                        ProductPrice = gamePrice,
                                        OrderNumber = orderNumber,
                                    };

                                    db.Orders.Add(order);

                                    break;
                                }
                            }
                        }
                    }

                    //It's work if one or more product valide,
                    if (isOrderValide)
                    {
                        orderNumber.TotalPrice = totalPrice;

                        db.OrderNumbers.Add(orderNumber);
                        HttpContext.Session.Remove("CartId");
                        HttpContext.Session.Remove("CountOfGoods");
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Cart", model);
        }
    }
}
