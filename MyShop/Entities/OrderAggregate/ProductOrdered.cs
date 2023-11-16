using Microsoft.EntityFrameworkCore;

namespace MyShop.Entities.OrderAggregate
{
    [Owned]
    public class ProductOrdered
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
    }
}