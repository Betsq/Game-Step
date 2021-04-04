using Game_Step.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewComponents
{
    public class ScreenshotViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {

        private readonly ApplicationContext db;

        public ScreenshotViewComponent(ApplicationContext context)
        {
            db = context;
        }

        public  IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
