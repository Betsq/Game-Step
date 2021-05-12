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
        public async Task<IActionResult> Update(SliderViewModel model)
        {
            var slider = _db.MainItemSliders.Include(item => item.AdditionalItem)
                .FirstOrDefault(item => item.Id == model.MainSlider.Id);

            if (slider == null)
                return View(model);

            await UpdateImage(slider, slider.Id, model.MainSliderImage);
            await UpdateImage(slider.AdditionalItem[0], slider.Id, model.SecondSliderImage);
            await UpdateImage(slider.AdditionalItem[1], slider.Id, model.ThirtySliderImage);

            await UpdateItem(model.MainSlider, slider);
            await UpdateItem(model.SecondSlider, slider.AdditionalItem[0]);
            await UpdateItem(model.ThirtySlider, slider.AdditionalItem[1]);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task UpdateItem(Slider model, Slider itemSlider)
        {
            if (itemSlider == null)
                return;

            itemSlider.ItemName = model.ItemName;

            itemSlider.Link = model.Link;

            if (model.IsGame)   
            {
                var game = await _db.Games
                    .Include(item => item.GamePrice)
                    .FirstOrDefaultAsync(item => item.Id == model.GameId);

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
        public async Task UpdateImage(Slider slider, int idMainSlider, IFormFile file)
        {
            if(file == null)
                return;

            string path = _appEnvironment.WebRootPath + "/Files/Slider/" + idMainSlider;
            string pathToImage = "/Files/Slider/" + idMainSlider + "/" + file.Name + ".jpg";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            await using var filesStream = new FileStream(_appEnvironment.WebRootPath + pathToImage, FileMode.Create);
            await file.CopyToAsync(filesStream);

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
