using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Entities
{

    [Table("MyShopUserAddress")]
    public class UserAddress : Address
    {
        public int Id { get; set; }
    }
}