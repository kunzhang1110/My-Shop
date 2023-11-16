using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Entities.OrderAggregate
{
    [Table("MyShopOrderItems")]
    public class OrderItem
    {
        public int Id { get; set; }
        public ProductOrdered ItemOrdered { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
    }
}