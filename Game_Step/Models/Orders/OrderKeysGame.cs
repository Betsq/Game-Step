namespace Game_Step.Models.Orders
{
    public class OrderKeysGame
    {
        public int Id { get; set; }
        public string Key { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
