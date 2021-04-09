﻿using Game_Step.Models;
using Game_Step.Models.Comments;
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
        private readonly ApplicationContext db;

        public CommentController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddMainComment(GameViewModel model)
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

            return RedirectToAction("Game", "Game", new {id = model.Game.Id});
        }

        [HttpPost]
        public async Task<IActionResult> AddSubComment(GameViewModel model)
        {
            var game = await db.Games.FirstOrDefaultAsync(item => item.Id == model.Game.Id);
            if (game != null)
            {
                var mainComment = await db.MainComments.FirstOrDefaultAsync(item => item.Id == model.MainComment.Id);
                if (mainComment != null)
                {
                    SubComment subComment = new SubComment
                    {
                        Message = model.MainComment.Message,
                        TimeCreated = DateTime.Now,
                        MainComment = mainComment,
                    };

                    await db.SubComments.AddAsync(subComment);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Game", "Game", new {id = model.Game.Id} );
        }

        [HttpPost]
        public JsonResult RemoveMainComment(int? id)
        {
            if (id != null)
            {
                var mainComment = db.MainComments
                                .FirstOrDefault(item => item.Id == id);

                if (mainComment != null)
                {
                    db.MainComments.Remove(mainComment);
                    db.SaveChanges();

                    return Json(true);
                }

            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult RemoveSubComment(int? id)
        {
            if (id != null)
            {
                var subComment = db.SubComments
                                .FirstOrDefault(item => item.Id == id);

                if (subComment != null)
                {
                    db.SubComments.Remove(subComment);
                    db.SaveChanges();

                    return Json(true);
                }

            }
            return Json(false);
        }
    }
}

