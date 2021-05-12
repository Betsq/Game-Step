using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Game_Step.Models;
using Game_Step.Models.Slider;
using Game_Step.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Game_Step.Controllers
{
    public class SliderController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public SliderController(ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IActionResult Index() => View(_db.MainItemSliders.ToList());

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var slider = await _db.MainItemSliders
                .Include(item => item.AdditionalItem)
                .FirstOrDefaultAsync(item => item.Id == id);

            var game = await _db.Games.ToListAsync();

            if (slider == null)
                return NotFound();

            var model = new SliderViewModel()
            {
                MainSlider = slider,
                SecondSlider = slider.AdditionalItem?[0],
                ThirtySlider = slider.AdditionalItem?[1],
                Game = game
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(SliderViewModel model)
        {
            var slider = _db.MainItemSliders.Include(item => item.AdditionalItem)
                .FirstOrDefault(item => item.Id == model.MainSlider.Id);

            if (slider == null)
                return View(model);

            UpdateImage(slider, slider.Id, model.MainSliderImage);
            UpdateImage(slider.AdditionalItem[0], slider.Id, model.SecondSliderImage);
            UpdateImage(slider.AdditionalItem[1], slider.Id, model.ThirtySliderImage);

            UpdateItem(model.MainSlider, slider);
            UpdateItem(model.SecondSlider, slider.AdditionalItem[0]);
            UpdateItem(model.ThirtySlider, slider.AdditionalItem[1]);

            _db.MainItemSliders.Update(slider);
            _db.AdditionalItemSliders.Update(slider.AdditionalItem[0]);
            _db.AdditionalItemSliders.Update(slider.AdditionalItem[1]);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public void UpdateItem(Slider model, Slider itemSlider)
        {
            if (itemSlider == null)
                return;

            itemSlider.ItemName = string.IsNullOrEmpty(model.ItemName) ? null : model.ItemName;

            itemSlider.Link = model.Link;

            if (model.IsGame)   
            {
                var game = _db.Games
                    .Include(item => item.GamePrice)
                    .FirstOrDefault(item => item.Id == model.GameId);

                if (game != null)
                {
                    itemSlider.Game = game;
                    itemSlider.IsGame = true;
                }
            }
            else
            {
                itemSlider.GameId = null;
                itemSlider.IsGame = false;
            }
                

            UpdateDataBase(itemSlider);
        }
        public void UpdateImage(Slider slider, int idMainSlider, IFormFile file)
        {
            if(file == null)
                return;

            string path = _appEnvironment.WebRootPath + "/Files/Slider/" + idMainSlider;
            string pathToImage = "/Files/Slider/" + idMainSlider + "/" + file.Name + ".jpg";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using var filesStream = new FileStream(_appEnvironment.WebRootPath + pathToImage, FileMode.Create);
            file.CopyTo(filesStream);

            slider.ItemImage = pathToImage;
            UpdateDataBase(slider);
        }

        public void UpdateDataBase(Slider slider)
        {
            switch (slider)
            {
                case MainItemSlider mainItem:
                    _db.MainItemSliders.Update(mainItem);
                    break;
                case AdditionalItemSlider additionalItem:
                    _db.AdditionalItemSliders.Update(additionalItem);
                    break;
            }
        }
    }
}
