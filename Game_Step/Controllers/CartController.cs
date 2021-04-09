﻿using Game_Step.Models;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game_Step.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationContext db;
        public CartController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listId = HttpContext.Session.Get<List<int>>("CartId");
            List<Cart> carts = new List<Cart>();

            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var game = db.Games.Include(price => price.GamePrice)
                        .Include(image => image.GameImage)
                        .FirstOrDefault(game => game.Id == id);

                    if (game != null)
                    {
                        Cart cart = new Cart
                        {
                            Id = game.Id,
                            Name = game.Name,
                            Quantity = game.QuantityOfGoods,
                            PlatformActivate = game.WhereKeyActivated,
                            Region = game.Region,
                            Price = game.GamePrice.Price,
                            IsDiscount = game.GamePrice.IsDiscount,
                            Discount = game.GamePrice.Discount,
                            DiscountPrice = game.GamePrice.DiscountPrice,
                            Image = game.GameImage.ImageInCatalog,
                        };
                        carts.Add(cart);
                    }
                }   
            }

            CartOrderViewModel cartOrder = new CartOrderViewModel
            {
                InCart = carts,
            };

            return View(cartOrder);
        }



        [HttpGet]
        public IActionResult CallCart()
        {
            return ViewComponent("PopupCart");
        }

        [HttpPost]
        public JsonResult CountOfGoods()
        {
            //if CountOfGoods is set in the session, then we return json with the number of items in the cart.
            //Otherwise, we return an empty string.
            if (HttpContext.Session.Keys.Contains("CountOfGoods"))
            {
                var countOfGoods = HttpContext.Session.Get<int>("CountOfGoods");

                //If the number of products is 0 or less, then delete the session object
                if (countOfGoods <= 0)
                {
                    HttpContext.Session.Remove("CountOfGoods");
                    return Json(0);
                }

                return Json(countOfGoods);
            }

            return Json(0);
        }

        [HttpPost]
        public JsonResult IsProsuctInCart(int id)
        {
            if (HttpContext.Session.Keys.Contains("CartId"))
            {
                var listId = HttpContext.Session.Get<List<int>>("CartId");

                //Check if the incoming "id" is already in the list
                //DON'T FORGET TO CHANGE
                foreach (var lsId in listId)
                {
                    if (lsId == id)
                    {
                        return Json(true);
                    }
                }
            }
            return Json(false);
        }

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            bool isHaveIdinList = false;
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
                    if (lsId == id)
                    {
                        isHaveIdinList = true;
                        break;
                    }
                }

                //if "id" in the list is not found, we add "id" to the list
                if (isHaveIdinList == false)
                {
                    listId.Add(id);
                    HttpContext.Session.Set("CartId", listId);
                }
            }
            //If the "cart" session is not set
            else
            {
                //Create a new list
                List<int> listId = new List<int>();
                listId.Add(id);

                //Set a session
                HttpContext.Session.Set("CartId", listId);
            }

            if (HttpContext.Session.Keys.Contains("CountOfGoods") && isHaveIdinList == false)
            {
                countOfGoods = HttpContext.Session.Get<int>("CountOfGoods");
                //add the number of items in the cart
                countOfGoods += 1;
                HttpContext.Session.Set("CountOfGoods", countOfGoods);
            }
            else
            {
                if (isHaveIdinList == false)
                    HttpContext.Session.Set("CountOfGoods", 1);
            }

            return ViewComponent("PopupCart");
        }

        [HttpGet]
        public IActionResult DeleteProductInCart(int id)
        {
            if (HttpContext.Session.Keys.Contains("CartId"))
            {
                //get the list with id
                List<int> listId = HttpContext.Session.Get<List<int>>("CartId");

                //Check if the passed "id" is in the list, if true, remove "id" from the list
                foreach (var lsId in listId.ToList())
                {
                    if (lsId == id)
                    {
                        listId.Remove(id);
                        HttpContext.Session.Set<List<int>>("CartId", listId);

                        //Get item quantity
                        int countOfGoods = HttpContext.Session.Get<int>("CountOfGoods");
                        //Decrease the quantity of goods by 1
                        countOfGoods -= 1;
                        //Set the session object with a new value
                        HttpContext.Session.Set("CountOfGoods", countOfGoods);

                    }
                }
            }
            return ViewComponent("PopupCart");
        }

        [HttpPost]
        public JsonResult CountPrice(int? id, int? amountProduct)
        {
            if (id != null)
            {
                if (amountProduct != null)
                {
                    //If the "cart" session is set
                    if (HttpContext.Session.Keys.Contains("CartId"))
                    {
                        //get the list with id
                        var listId = HttpContext.Session.Get<List<int>>("CartId");

                        //Check if the incoming "id" is already in the list
                        //DON'T FORGET TO CHANGE
                        foreach (var lsId in listId)
                        {
                            if (lsId == id)
                            {
                                var gamePrice = db.GamePrices.FirstOrDefault(item => item.GameId == id);
                                if (gamePrice.IsDiscount)
                                {
                                    return Json(gamePrice.DiscountPrice * amountProduct);
                                }
                                else
                                {
                                    return Json(gamePrice.Price * amountProduct);
                                }
                            }
                        }
                    }
                }
            }
            return Json("");
        }

    }
}
