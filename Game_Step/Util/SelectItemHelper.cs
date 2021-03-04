using Game_Step.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                items.Add(new SelectListItem { Text = dev.Name, Value = dev.Name });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GerPublisher()
        {
            var publisher = await db.Publishers.ToListAsync();

            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var pub in publisher)
            {
                items.Add(new SelectListItem { Text = pub.Name, Value = pub.Name });
            }

            return items;
        }
    }
}
