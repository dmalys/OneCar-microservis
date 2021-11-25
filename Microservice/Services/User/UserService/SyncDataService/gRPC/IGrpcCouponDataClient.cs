using AccountService.Proto;
using CouponService.Proto;

namespace UserService.SyncDataService.gRPC
{
    public interface IGrpcCouponDataClient
    {
        void NotifyDeleteCouponEntity(NotifyDeleteCouponRequest request);
        CouponDTO GetCouponData(GetCouponDataRequest request);
    }
}
