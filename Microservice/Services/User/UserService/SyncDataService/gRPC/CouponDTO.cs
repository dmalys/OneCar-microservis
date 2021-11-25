using System;

namespace UserService.SyncDataService.gRPC
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public float MoneyValue { get; set; }
    }
}
