using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Entities;
using MyShop.Entities.OrderAggregate;

namespace MyShop.Data
{
    public class MyShopContext : IdentityDbContext<User, Role, int>
    {

        public MyShopContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(a => a.UserAddress)
                .WithOne()
                .HasForeignKey<UserAddress>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //create new roles
            builder.Entity<Role>()
              .HasData(
                  new Role { Id = 1, Name = "Member", NormalizedName = "MEMBER" },
                  new Role { Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
              );
        }
    }
}
