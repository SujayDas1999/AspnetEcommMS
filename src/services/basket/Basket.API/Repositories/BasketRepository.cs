using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        // injecting redis cache
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task DeleteBasket(string username)
        {
           await _cache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> Get(string username)
        {
           var basket =  await _cache.GetStringAsync(username);

            if (string.IsNullOrEmpty(basket)) return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> Update(ShoppingCart cart)
        {
            var basket = JsonConvert.SerializeObject(cart);

            await _cache.SetStringAsync(cart.UserName, basket);

            return await Get(cart.UserName);
        }
    }
}
