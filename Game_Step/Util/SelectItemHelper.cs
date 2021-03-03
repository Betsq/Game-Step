using Game_Step.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Util
{
    public class SelectItemHelper
    {
        private readonly ApplicationContext db;

        public SelectItemHelper(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<SelectListItem> GetDeveloper()
        {
            var developer = db.Developers.ToList();

            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var dev in developer)
            {
                items.Add(new SelectListItem {Text = dev.Name, Value = dev.Name });
            }    
            return items;
        }
    }
}
