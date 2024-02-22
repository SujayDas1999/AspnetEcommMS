
using Discount.GRPC.Entities;
using Discount.GRPC.Entities.Dtos;
using System.Collections.Generic;

namespace Discount.GRPC.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> Get(string productname);
        Task<CouponCreateReturnDto> Save(CouponDto coupondto);
        Task<bool> Update(Guid id, CouponDto coupondto);
        Task<bool> Delete(Guid id);
        
    }
}
