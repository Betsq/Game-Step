using Game_Step.Models.Comments;
using System.Collections.Generic;

namespace Game_Step.ViewModels
{
    public class CommentCheckingViewModel
    {
       public List<MainComment> MainComment { get; set; }

        public Dictionary<int, bool> Items { get; set; }
    }
}
