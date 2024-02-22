
using Discount.GRPC.Data;
using Discount.GRPC.Entities;
using Discount.GRPC.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly DiscountContext _context;

        public CouponRepository(DiscountContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(Guid id)
        {
            var deletedCoupon = await _context.Coupons.FindAsync(id);
            if (deletedCoupon != null)
            {
                return false;
            }

            _context.Coupons.Remove(deletedCoupon);
            var isSuccess = await _context.SaveChangesAsync() > 0;
            if (isSuccess) return true;
            else return false;
        }

        public async Task<Coupon> Get(string productname)
        {
           
           var allcoupons = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName.ToLower() == productname.ToLower());
           return allcoupons;
            

            //if(string.IsNullOrWhiteSpace(productname)) throw new ArgumentNullException(nameof(productname));
            //var coupons = await _context.Coupons.Where(x => x.ProductName.ToLower() == productname.ToLower()).ToListAsync();
            //return coupons;
        }

        public async Task<CouponCreateReturnDto> Save(CouponDto coupondto)
        {
            var coupon = new Coupon
            {
                Amount = coupondto.Amount,
                Description = coupondto.Description,
                ProductName = coupondto.ProductName,
            };

            _context.Coupons.Add(coupon);
            var isSuccess = await _context.SaveChangesAsync() > 0;
            if (isSuccess) return new CouponCreateReturnDto
            {
                IsSuccess = true,
                Id = coupon.ID.ToString(),
            };

            return new CouponCreateReturnDto{ IsSuccess = false };

        }

        public async Task<bool> Update(Guid id, CouponDto coupondto)
        {
            var coupon = await _context.Coupons.FindAsync(id);


            coupon.Amount = coupondto.Amount;
            coupon.Description = coupondto.Description;
            coupon.ProductName = coupondto.ProductName;
           

            _context.Coupons.Update(coupon);
            //_context.Entry(coupon).State = EntityState.Modified;
            var isSuccess = await _context.SaveChangesAsync() > 0;
            if (isSuccess) return true;
            else return false;
            
        }
    }
}
