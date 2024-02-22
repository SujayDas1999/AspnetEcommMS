using Discount.GRPC.Data;
using Discount.GRPC.Entities;
using Discount.GRPC.Entities.Dtos;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;

namespace Discount.GRPC.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;

        public DiscountService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupons = await _couponRepository.Get(request.ProductName) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product code {request.ProductName} not found"));

            return new CouponModel
            {
                Amount = coupons.Amount,
                ProductName = coupons.ProductName,
                Description = coupons.Description,
                Id = coupons.ID.ToString(),
            };
            
            //var couponModel = new List<CouponModel>();

            //foreach(var coupon in coupons)
            //{
            //    var model = new CouponModel
            //    {
            //        Id = coupon.ID.ToString(),
            //        Amount = coupon.Amount,
            //        Description = coupon.Description,
            //        ProductName = coupon.ProductName
            //    };
            //    couponModel.Add(model);

            //}

            //CouponModelList modelList = new CouponModelList();
            //modelList.CouponModel.AddRange(couponModel);
            //return modelList;
        }

        public override async Task<CreateCouponResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponDto = new Entities.Dtos.CouponDto
            {
                Amount = request.CouponDto.Amount,
                Description = request.CouponDto.Description,
                ProductName = request.CouponDto.ProductName
            };
            var response =  await _couponRepository.Save(couponDto);

            if (response.IsSuccess)
            {
                return new CreateCouponResponse
                {
                    Id = response.Id,
                    Success = true
                };
            }
            return new CreateCouponResponse
            {
                Id = string.Empty,
                Success = false
            };
        }

        public override async Task<UpdateCouponResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            try
            {
                var id = new Guid(request.Id);
                var couponDto = new Entities.Dtos.CouponDto
                {
                    Amount = request.CouponDto.Amount,
                    Description = request.CouponDto.Description,
                    ProductName = request.CouponDto.ProductName
                };

                var isSuccess = await _couponRepository.Update(id, couponDto);
                if(isSuccess)
                {
                    return new UpdateCouponResponse
                    {
                        Success = true
                    };
                } 

                return new UpdateCouponResponse { Success = false };
            }
            catch (FormatException ex)
            {

                throw new RpcException(new Status(StatusCode.Aborted, $"Bad Guid {request.Id} passed with message {ex.Message}"));
            }
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            try
            {
                var id = new Guid(request.Id);

                var isSuccess = await _couponRepository.Delete(id);

                //return is

                return new DeleteDiscountResponse 
                {
                  Success = isSuccess
                };
            }
            catch (FormatException ex)
            {

                throw new RpcException(new Status(StatusCode.Aborted, $"Bad Guid {request.Id} passed with message {ex.Message}"));
            }
        }

    }
}
