using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Entities
{
    [Table("MyShopProducts")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }
        public string PublicId { get; set; } //publicId for image of a product
    }
}
