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

        public IEnumerable<SelectListItem> GetPublisher()
        {
            var publisher = db.Publishers.ToList();

            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var pub in publisher)
            {
                items.Add(new SelectListItem { Text = pub.Name, Value = pub.Name });
            }

            return items;
        }

        public static IEnumerable<SelectListItem> GetWhereKeyActivated()
        {
            IList<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Steam", Value = "Steam"},
                new SelectListItem{Text = "GOG", Value = "GOG"},
                new SelectListItem{Text = "Origin", Value = "Origin"},
                new SelectListItem{Text = "Epic Games Store", Value = "Epic Games Store"},
            };

            return items;
        }

        public static IEnumerable<SelectListItem> GetGenre()
        {
            IList<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Steam", Value = "Steam"},
                new SelectListItem{Text = "GOG", Value = "GOG"},
                new SelectListItem{Text = "Origin", Value = "Origin"},
                new SelectListItem{Text = "Epic Games Store", Value = "Epic Games Store"},
                new SelectListItem{Text = "Suck", Value = "Suck"},
                new SelectListItem{Text = "Sos", Value = "Sos"},
                new SelectListItem{Text = "Dick", Value = "Dick"},
                new SelectListItem{Text = "Epic", Value = "Epic"},
            };
            return items;
        }
    }
}
