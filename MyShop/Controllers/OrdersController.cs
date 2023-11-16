using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.DTOs;
using MyShop.Entities;
using MyShop.Entities.OrderAggregate;
using MyShop.Extensions;

namespace MyShop.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly MyShopContext _context;
        private readonly IMapper _mapper;

        public OrdersController(MyShopContext context, IMapper mappper)
        {
            _context = context;
            _mapper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(order => order.OrderItems)
                .Where(x => x.BuyerId == User.Identity.Name)
                .ToListAsync();

            return _mapper.Map<List<Order>, List<OrderDto>>(orders);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            return _mapper.Map<OrderDto>(
                await _context.Orders
                    .FirstOrDefaultAsync(x => x.BuyerId == User.Identity.Name && x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderDto orderDto)
        {
            var basket = await _context.Baskets
                .RetrieveBasketWithItems(User.Identity.Name)
                .FirstOrDefaultAsync();

            if (basket == null) return BadRequest(new ProblemDetails
            {
                Title = "Could not find basket"
            });


            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                var orderItem = new OrderItem
                {
                    ItemOrdered = new ProductOrdered
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
                product.QuantityInStock -= item.Quantity;
            }

            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var deliveryFee = subtotal > 10000 ? 0 : 500;

            var order = new Order
            {
                OrderItems = items,
                BuyerId = User.Identity.Name,
                ShippingAddress = orderDto.ShippingAddress,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
                PaymentIntentId = basket.PaymentIntentId
            };

            _context.Orders.Add(order);
            _context.Baskets.Remove(basket); //remove user basket

            //save user address
            if (orderDto.SaveAddress)
            {
                var user = await _context.Users
                    .Include(a => a.UserAddress)
                    .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

                user.UserAddress = new UserAddress
                {
                    FullName = orderDto.ShippingAddress.FullName,
                    Address1 = orderDto.ShippingAddress.Address1,
                    Address2 = orderDto.ShippingAddress.Address2,
                    City = orderDto.ShippingAddress.City,
                    State = orderDto.ShippingAddress.State,
                    Zip = orderDto.ShippingAddress.Zip,
                    Country = orderDto.ShippingAddress.Country
                };
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetOrder", new { id = order.Id }, order.Id);//reutrn /order/{id}

            return BadRequest("Problem creating order");
        }

    }
}