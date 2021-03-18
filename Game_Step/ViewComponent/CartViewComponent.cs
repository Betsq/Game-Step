using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Game_Step.ViewComponent
{
    [ViewComponent]
    public class CartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
