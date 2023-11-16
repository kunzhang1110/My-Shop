using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.DTOs;
using MyShop.Entities;
using MyShop.Extensions;

namespace MyShop.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly MyShopContext _context;
        private readonly IMapper _mapper;

        public BasketController(MyShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Basket> RetrieveBasket()
        {
            var buyerId = User.Identity?.Name ?? Request.Cookies["buyerId"];
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }
            return await _context.Baskets
                .RetrieveBasketWithItems(buyerId)
                .FirstOrDefaultAsync();
        }

        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;

            if (string.IsNullOrEmpty(buyerId)) //if user is not logged in
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions
                {
                    Secure = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(30),
                    SameSite = SameSiteMode.None
                };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }

            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            return _mapper.Map<BasketDto>(basket);
        }


        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            var basket = await RetrieveBasket();

            if (basket == null) basket = CreateBasket();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return BadRequest(new ProblemDetails { Title = "Product not found" });

            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetBasket", _mapper.Map<BasketDto>(basket));//"GetBasket" is the url to get the created basket

            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity = 1)
        {
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            basket.RemoveItem(productId, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }
    }
}