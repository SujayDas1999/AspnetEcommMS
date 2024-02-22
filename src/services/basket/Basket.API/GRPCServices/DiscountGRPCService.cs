using Discount.GRPC.Protos;

namespace Basket.API.GRPCServices
{
    public class DiscountGRPCService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountGrpcClient;

        public DiscountGRPCService(DiscountProtoService.DiscountProtoServiceClient discountGrpcClient)
        {
            _discountGrpcClient = discountGrpcClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            
                return await _discountGrpcClient.GetDiscountAsync(discountRequest);
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //}

            //return new CouponModel();

        }
    }
}
