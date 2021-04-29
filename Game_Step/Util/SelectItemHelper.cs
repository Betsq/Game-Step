using Game_Step.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Game_Step.Util
{
    public class SelectItemHelper
    {
        private readonly ApplicationContext _db;

        public SelectItemHelper(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<SelectListItem> GetDeveloper()
        {
            var developer = _db.Developers.ToList();

            return developer.Select(dev => new SelectListItem {Text = dev.Name, Value = dev.Name}).ToList();
        }

        public IEnumerable<SelectListItem> GetPublisher()
        {
            var publisher = _db.Publishers.ToList();

            return publisher.Select(pub => new SelectListItem {Text = pub.Name, Value = pub.Name}).ToList();
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
            };
            return items;
        }

        public IEnumerable<SelectListItem> GetGameId()
        {
            var game = _db.Games.ToList();

            return game.Select(g => new SelectListItem {Text = g.Name, Value = g.Id.ToString()}).ToList();
        }
    }
}
