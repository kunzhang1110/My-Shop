using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.DTOs;
using MyShop.Entities.OrderAggregate;
using MyShop.Extensions;
using MyShop.Services;
using Stripe;

namespace MyShop.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly PaymentService _paymentService;
        private readonly MyShopContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PaymentsController(PaymentService paymentService, MyShopContext context,
            IConfiguration config, IMapper mapper)
        {
            _config = config;
            _context = context;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
        {
            var basket = await _context.Baskets
                .RetrieveBasketWithItems(User.Identity.Name)
                .FirstOrDefaultAsync();

            if (basket == null) return NotFound();

            var intent = await _paymentService.CreateOrUpdatePaymentIntent(basket);

            if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

            //save paymentIntent and ClientSecret to db
            basket.PaymentIntentId ??= intent.Id;
            basket.ClientSecret ??= intent.ClientSecret;
            _context.Update(basket);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });

            return _mapper.Map<BasketDto>(basket);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                _config["StripeSettings:WebhookSecret"]);


            var charge = (Charge)stripeEvent.Data.Object;

            var order = await _context.Orders.FirstOrDefaultAsync(x =>
                x.PaymentIntentId == charge.PaymentIntentId);

            Console.WriteLine("here" + charge.PaymentIntentId);
            if (charge.Status == "succeeded") order.OrderStatus = OrderStatus.PaymentReceived;

            await _context.SaveChangesAsync();

            return new EmptyResult();
        }


    }
}