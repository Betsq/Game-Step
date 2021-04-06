using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.Models.Comments
{
    public class MainComment : Comment
    {
        public List<SubComment> SubComments { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
