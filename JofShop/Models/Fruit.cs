using JofShop.Models.Common;

namespace JofShop.Models
{
    public class Fruit:BaseEntity
    {
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}
