using CouponService.DataAccessLayer.Interfaces;
using Grpc.Core;
using CouponService.Proto;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace CouponService.SyncDataServices.gRPC
{
    public class GrpcCouponService : GrpcCoupon.GrpcCouponBase
    {
        private readonly ICouponRepository _couponRepository;

        public GrpcCouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public override Task<NotifyDeleteCouponResponse> NotifyDeleteCouponEntity(NotifyDeleteCouponRequest request, ServerCallContext context)
        {
            _couponRepository.DeleteAsync(request.CouponId);

            return Task.FromResult(new NotifyDeleteCouponResponse());
        }

        public override Task<GetCouponDataResponse> GetCouponData(GetCouponDataRequest request, ServerCallContext context)
        {
            var coupon = _couponRepository.GetAsyncByCode(request.CouponCode).Result;

            var response = new GetCouponDataResponse
            {
                CouponId = coupon.CouponId,
                ExpirationDate = coupon.ExpirationDate.ToTimestamp(),
                MoneyValue = coupon.MoneyValue
            };
            return Task.FromResult(response);
        }
    }
}
