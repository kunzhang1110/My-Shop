using Microsoft.AspNetCore.Identity;

namespace MyShop.Entities
{
    /// <summary>
    /// User class for ASP.NET Identity
    /// </summary>
    public class User : IdentityUser<int>
    {
        public UserAddress UserAddress { get; set; }
    }
}
