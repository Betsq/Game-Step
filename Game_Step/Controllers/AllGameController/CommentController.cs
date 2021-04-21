using Game_Step.Models;
using Game_Step.Models.Comments;
using Game_Step.ViewModels;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Controllers.AllGameController
{
    public class CommentController : Controller
    {
        private readonly ApplicationContext _db;

        public CommentController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddMainComment(GameViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            var game = await _db.Games.FirstOrDefaultAsync(item => item.Id == model.Game.Id);

            if (game == null)
                return Json(false);

            var mainComment = new MainComment
            {
                Message = model.MainComment.Message,
                TimeCreated = DateTime.Now,
                Game = game,
            };

            await _db.MainComments.AddAsync(mainComment);
            await _db.SaveChangesAsync();
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubComment(GameViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            var game = await _db.Games.FirstOrDefaultAsync(item => item.Id == model.Game.Id);

            if (game == null)
                return Json(false);

            var mainComment = await _db.MainComments.FirstOrDefaultAsync(item => item.Id == model.MainComment.Id);
            if (mainComment == null)
                return Json(false);

            var subComment = new SubComment
            {
                Message = model.MainComment.Message,
                TimeCreated = DateTime.Now,
                MainComment = mainComment,
            };

            await _db.SubComments.AddAsync(subComment);
            await _db.SaveChangesAsync();
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveMainComment(int? id)
        {
            if (id == null)
                return Json(false);

            var mainComment = _db.MainComments
                .FirstOrDefault(item => item.Id == id);

            if (mainComment == null)
                return Json(false);

            _db.MainComments.Remove(mainComment);
            await _db.SaveChangesAsync();

            return Json(true);
        }

        [HttpPost]
        public JsonResult RemoveSubComment(int? id)
        {
            if (id == null)
                return Json(false);

            var subComment = _db.SubComments
                .FirstOrDefault(item => item.Id == id);

            if (subComment == null) 
                return Json(false);

            _db.SubComments.Remove(subComment);
            _db.SaveChanges();

            return Json(true);
        }
    }
}


