namespace Game_Step.Models.GamesModel
{
    public class GameImage
    {
        public int Id { get; set; }
        public string MainImage { get; set; }
        public string InnerImage { get; set; }
        public string ImageInCatalog { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
