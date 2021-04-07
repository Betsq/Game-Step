using Game_Step.Models;
using Game_Step.Models.Comments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels.GamesViewModel
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public MainComment MainComment { get; set; }
    }
}
