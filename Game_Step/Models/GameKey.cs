using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models
{
    public class GameKey
    {
        public int Id { get; set; }

        public string KeyToGame { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
