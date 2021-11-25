using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CouponService.BusinessLayer.Coupon.Interfaces;
using CouponService.BusinessLayer.Coupon.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ILogger<CouponController> _logger;
        private readonly ICouponHandler _couponHandler;

        public CouponController(ILogger<CouponController> logger,
             ICouponHandler couponHandler)
        {
            _logger = logger;
            _couponHandler = couponHandler;
        }

        [HttpPost]
        [Route("GetCoupons")]
        [SwaggerOperation(OperationId = "GetCoupons")]
        [ProducesResponseType(typeof(GetCouponsResponse), StatusCodes.Status200OK)]
        public async Task<GetCouponsResponse> GetCoupons()
        {
            return await _couponHandler.GetCoupons();
        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetCouponResponse), StatusCodes.Status200OK)]
        public async Task<GetCouponResponse> GetDetails([FromBody] GetCouponRequest request)
        {
            return await _couponHandler.GetCoupon(request);
        }

        [HttpPost]
        [Route("AddCoupon")]
        [SwaggerOperation(OperationId = "AddCoupon")]
        public async Task AddCar([FromBody]AddCouponRequest request)
        {
            await _couponHandler.AddCoupon(request);
        }

        [HttpPost]
        [Route("DeleteCoupon")]
        [SwaggerOperation(OperationId = "DeleteCoupon")]
        public async Task DeleteCoupon([FromBody]DeleteCouponRequest request)
        {
            await _couponHandler.DeleteCoupon(request);
        }
    }
}
