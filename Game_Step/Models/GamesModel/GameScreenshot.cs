namespace Game_Step.Models.GamesModel
{
    public class GameScreenshot
    {
        public int Id { get; set; }
        public string Screenshot { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
