using Game_Step.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels.GamesViewModel
{
    public class CommentsViewModel : Comment
    {
        public int GameId { get; set; }
    }
}
