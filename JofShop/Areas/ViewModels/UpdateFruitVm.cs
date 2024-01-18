namespace JofShop.Areas.ViewModels
{
    public class UpdateFruitVm
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
