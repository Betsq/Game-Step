using Game_Step.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Order(Dictionary<int, int> items)
        {
            if (items != null)
            {
                var listProdId = HttpContext.Session.Get<List<int>>("CountOfGoods");
                foreach (var item in items)
                {
                    foreach (var prodId in listProdId)
                    {
                        if (item.Key == prodId)
                        {
                            Guid.NewGuid();
                        }
                    }
                }
            }
            return View();
        }
    }
}
