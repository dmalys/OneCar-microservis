using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.BusinessLayer.Coupon.Models
{
    public class UpdateCouponRequest : CouponDetailedRequest
    {
        public string UpdatedBy { get; set; }
        public int CouponId { get; set; }
    }
}
