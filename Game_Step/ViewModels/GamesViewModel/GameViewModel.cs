using Game_Step.Models;
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
    }
}
