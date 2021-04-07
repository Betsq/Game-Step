using Game_Step.Models;
using Game_Step.Models.Comments;
using Game_Step.ViewModels.GamesViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Game_Step.Controllers.AllGameController
{
    public class CommentController : Controller
    {
        private readonly ApplicationContext db;

        public CommentController(ApplicationContext db)
        {
            this.db = db;
        }

        public async System.Threading.Tasks.Task<IActionResult> AddMainComment(GameViewModel model)
        {
            var game = await db.Games.FirstOrDefaultAsync(item => item.Id == model.Game.Id);
            if (game != null)
            {
                MainComment mainComment = new MainComment
                {
                    Message = model.MainComment.Message,
                    TimeCreated = DateTime.Now,
                    Game = game,
                };

                await db.MainComments.AddAsync(mainComment);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}


