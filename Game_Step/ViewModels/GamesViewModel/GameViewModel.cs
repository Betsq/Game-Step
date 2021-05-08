using Game_Step.Models;
using Game_Step.Models.Comments;

namespace Game_Step.ViewModels.GamesViewModel
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public MainComment MainComment { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public int CountKeys { get; set; }
    }
}
