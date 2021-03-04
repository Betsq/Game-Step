using System;

namespace Game_Step.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Article { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Features { get; set; }
        public string Region { get; set; }
    }
}
