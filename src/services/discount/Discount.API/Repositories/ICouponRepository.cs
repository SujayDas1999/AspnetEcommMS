using Discount.API.Entities;
using Discount.API.Entities.Dtos;
using System.Collections.Generic;

namespace Discount.API.Repositories
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> Get(string productname);
        Task<bool> Save(CouponDto coupondto);
        Task<bool> Update(Guid id, CouponDto coupondto);
        Task<bool> Delete(Guid id);
        
    }
}
