using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit.Text;

namespace Game_Step.Models.GamesModel
{
    public class GameTags
    {
        public int Id { get; set; }
        public bool Show { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
