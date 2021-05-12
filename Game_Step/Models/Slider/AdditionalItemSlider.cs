using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Step.Models.Slider
{
    public class AdditionalItemSlider
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string Link { get; set; }
        public bool IsGame { get; set; }
        public int Discount { get; set; }
        public int OldPrice { get; set; }
        public int CurrentPrice { get; set; }
        public int MainItemSliderId { get; set; }
        public MainItemSlider MainItemSlider { get; set; }
    }
}
