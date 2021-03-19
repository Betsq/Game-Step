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
        public IActionResult AddToCart(int id)
        {
            if (HttpContext.Session.Keys.Contains("CartId"))
            {
                bool isHaveId = false;

                var listId = HttpContext.Session.Get<List<int>>("CartId");

                foreach (var lsId in listId)
                {
                    if (lsId == id)
                    {
                        isHaveId = true;

                    }
                }

                if (isHaveId == false)
                {
                    listId.Add(id);
                    HttpContext.Session.Set<List<int>>("CartId", listId);
                }
            }
            else
            {
                List<int> listId = new List<int>();
                listId.Add(id);

                HttpContext.Session.Set<List<int>>("CartId", listId);
            }
            return ViewComponent("Cart");
        }

        [HttpGet]
        public IActionResult CallCart()
        {
            return ViewComponent("Cart");
        }
    }
}
