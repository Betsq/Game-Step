using Microsoft.AspNetCore.Http;

namespace Game_Step.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] byteImage { get; set; }
        public IFormFile Image { get; set; }
    }
}
