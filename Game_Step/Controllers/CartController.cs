using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CallCart()
        {
            return ViewComponent("Cart");
        }

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            bool isHaveId = false;
            int CountOfGoods;

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
                        isHaveId = true;
                        break;
                    }
                }
                    
                //if "id" in the list is not found, we add "id" to the list
                if (isHaveId == false)
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

            if (HttpContext.Session.Keys.Contains("CountOfGoods") && isHaveId == false)
            {
                CountOfGoods = HttpContext.Session.Get<int>("CountOfGoods");
                //add the number of items in the cart
                CountOfGoods += 1;
                HttpContext.Session.Set("CountOfGoods", CountOfGoods);
            }
            else
            {
                if (isHaveId == false)
                    HttpContext.Session.Set("CountOfGoods", 1);
            }
            return ViewComponent("Cart");
        }

        [HttpGet]
        public IActionResult DeleteProductInCart(int id)
        {
            if (HttpContext.Session.Keys.Contains("CartId"))
            {
                var listId = HttpContext.Session.Get<List<int>>("CartId");

                foreach (var lsId in listId)
                {
                    if (lsId == id)
                    {
                        listId.Remove(id);
                        HttpContext.Session.Set<List<int>>("CartId", listId);
                        return ViewComponent("Cart");
                    }
                }
            }
            return ViewComponent("Cart");
        }
    }
}
