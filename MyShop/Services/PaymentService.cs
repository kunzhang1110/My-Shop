
using MyShop.Entities;
using Stripe;


namespace MyShop.Services
{
    /// <summary>
    /// Provides Stripe related payment services.
    /// </summary>
    public class PaymentService
    {

        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {

            _config = config;
        }


        public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Basket basket)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();
            var intent = new PaymentIntent();

            var subtotal = basket.Items.Sum(i => i.Quantity * i.Product.Price);
            var deliveryFee = subtotal > 1000 ? 0 : 500;

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //create new payment intent 
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = subtotal + deliveryFee,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions    //update ammount
                {
                    Amount = subtotal + deliveryFee,
                };
                intent = await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            return intent;
        }
    }
}
