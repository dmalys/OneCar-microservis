using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.BusinessLayer.Coupon.Models
{
    public abstract class CouponIdRequest
    {
        public int CouponId { get; set; }
    }
}
