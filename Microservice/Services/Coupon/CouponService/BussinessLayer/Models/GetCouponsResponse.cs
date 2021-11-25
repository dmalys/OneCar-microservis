using Newtonsoft.Json;
using CouponService.BusinessLayer.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.BusinessLayer.Coupon.Models
{
    public class GetCouponsResponse
    {
        [JsonProperty("couponList")]
        public List<CouponDTO> CouponList { get; set; }
    }
}
