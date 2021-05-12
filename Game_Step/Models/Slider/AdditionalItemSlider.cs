namespace Game_Step.Models.Slider
{
    public class AdditionalItemSlider : Slider
    {
        public int MainItemSliderId { get; set; }
        public MainItemSlider MainItemSlider { get; set; }
    }
}
