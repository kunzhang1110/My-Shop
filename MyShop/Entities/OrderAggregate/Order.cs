using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Entities.OrderAggregate
{
    [Table("MyShopOrders")]
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }

        [Required] //[Required] is required for [Owned]
        public ShippingAddress ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set; }
        public long Subtotal { get; set; }
        public long DeliveryFee { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public long GetTotal()
        {
            return Subtotal + DeliveryFee;
        }
    }
}