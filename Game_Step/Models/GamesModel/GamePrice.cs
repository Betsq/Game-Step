namespace Game_Step.Models.GamesModel
{
    public class GamePrice
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public bool IsDiscount { get; set; }
        public int Discount { get; set; }
        public int DiscountPrice { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
