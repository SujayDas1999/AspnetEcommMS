using Discount.API.Entities;
using Discount.API.Entities.Dtos;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public DiscountController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet("coupons/{productname?}")]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetAll(string productname = null)
        {
            return Ok(await _couponRepository.Get(productname));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CouponDto couponDto)
        {
            var status = await _couponRepository.Save(couponDto);
            if (status) return CreatedAtAction(nameof(GetAll), new { productName = couponDto.ProductName }, null);
            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CouponDto couponDto)
        {
            var status = await _couponRepository.Update(id, couponDto);
            if (status) return Ok("Update success");
            else return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var status = await _couponRepository.Delete(id);
            if (status) return Ok("Delete success");
            else return StatusCode(500);
        }
    }
}
