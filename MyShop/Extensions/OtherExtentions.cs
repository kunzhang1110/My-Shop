using Microsoft.EntityFrameworkCore;
using MyShop.Entities;
using MyShop.RequestHelpers;
using System.Text.Json;

namespace MyShop.Extensions
{
    public static class OtherExtentions
    {

        public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> query, string buyerId)
        {
            return query
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .Where(basket => basket.BuyerId == buyerId);
        }

        public static void AddPaginationHeader(this HttpResponse response, MetaData metData)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(metData, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination"); //expose header to cross-domain client
        }
    }
}
