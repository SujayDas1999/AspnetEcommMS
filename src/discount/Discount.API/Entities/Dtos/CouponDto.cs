using System.ComponentModel.DataAnnotations;

namespace Discount.API.Entities.Dtos
{
    public class CouponDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
