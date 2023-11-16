using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Entities
{
    [Table("MyShopBasketItems")]
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Basket Basket { get; set; }
    }
}