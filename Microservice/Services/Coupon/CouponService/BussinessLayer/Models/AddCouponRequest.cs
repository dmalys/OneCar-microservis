using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.BusinessLayer.Coupon.Models
{
    public class AddCouponRequest : CouponDetailedRequest
    {
        public string CreatedBy { get; set; }
    }
}
