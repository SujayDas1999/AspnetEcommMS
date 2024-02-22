using Basket.API.Entities;
using Basket.API.GRPCServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGRPCService _discountGRPCService;

        public BasketController(IBasketRepository basketRepository, DiscountGRPCService discountGRPCService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountGRPCService = discountGRPCService ?? throw new ArgumentNullException(nameof(discountGRPCService));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ShoppingCart>> Get(string username)
        {
            return Ok(await _basketRepository.Get(username) ?? new ShoppingCart(username));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> Update([FromBody] ShoppingCart shoppingCart)
        {
            foreach(var item in  shoppingCart.Items)
            {
                var coupon = await _discountGRPCService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _basketRepository.Update(shoppingCart));
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok("deleted");
        }
    }
}
