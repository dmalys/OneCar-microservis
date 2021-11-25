using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.BusinessLayer.Coupon.Models
{
    public abstract class CouponDetailedRequest
    {
        public string Code { get; set; }

        public float MoneyValue { get; set; }

        public bool Enabled { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
