using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> Get(string username);
        Task<ShoppingCart> Update(ShoppingCart cart);
        Task DeleteBasket(string username);
    }
}
