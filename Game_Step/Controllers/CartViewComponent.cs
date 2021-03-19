using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Game_Step.ViewComponent
{
    [ViewComponent]
    public class CartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        List<string> users;
        public CartViewComponent()
        {
            users = new List<string>
            {
                "Tom", "Tim", "Bob", "Sam"
            };
        }

        public IViewComponentResult Invoke()
        {
            return View(users);
        }
    }
}
