using Game_Step.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class CommentCheckingViewModel
    {
       public List<MainComment> MainComment { get; set; }

        public Dictionary<int, bool> Items { get; set; }
    }
}
