using CouponService.BusinessLayer.Coupon.Interfaces;
using CouponService.BusinessLayer.Coupon.Models;
using CouponService.BusinessLayer.ErrorHandling;
using CouponService.BusinessLayer.User.Models;
using CouponService.DataAccessLayer.Entities;
using CouponService.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponService.BusinessLayer.Coupon.Implementation
{
    public class CouponHandler : ICouponHandler
    {
        private readonly ICouponRepository _couponRepository;

        public CouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task AddCoupon(AddCouponRequest request)
        {
            ValidateRequest(request);

            var couponEntity = new CouponEntity();
            couponEntity.MoneyValue = request.MoneyValue;
            couponEntity.ExpirationDate = request.ExpirationDate;
            couponEntity.Enabled = request.Enabled;
            couponEntity.Code = request.Code;
            couponEntity.CreateDate = DateTime.UtcNow;
            couponEntity.CreatedBy = request.CreatedBy;

            try
            {
                await _couponRepository.Insert(couponEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCoupon failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateCoupon(UpdateCouponRequest request)
        {
            ValidateRequest(request);
            if(request.CouponId == 0)
            {
                throw new SystemBaseException("CouponId is not valid.", SystemErrorCode.ValidationError);
            }

            var couponEntity = new CouponEntity();
            couponEntity.MoneyValue = request.MoneyValue;
            couponEntity.ExpirationDate = request.ExpirationDate;
            couponEntity.Enabled = request.Enabled;
            couponEntity.Code = request.Code;
            couponEntity.UpdateDate = DateTime.UtcNow;
            couponEntity.UpdatedBy = request.UpdatedBy;
            couponEntity.CouponId = request.CouponId;

            try
            {
                if(await _couponRepository.CheckCouponExists(request.CouponId))
                {
                    await _couponRepository.Update(couponEntity);
                }
                else
                {
                    throw new SystemBaseException("Entity not found.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateCoupon failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task DeleteCoupon(DeleteCouponRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _couponRepository.CheckCouponExists(request.CouponId))
                {
                    await _couponRepository.DeleteAsync(request.CouponId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteCoupon failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCouponResponse> GetCoupon(GetCouponRequest request)
        {
            ValidateRequest(request);

            try
            {
                var couponEntity = await _couponRepository.GetAsync(request.CouponId);

                if(couponEntity == null)
                {
                    return new GetCouponResponse();
                }

                return new GetCouponResponse
                {
                    Coupon = ConvertFromEntity(couponEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCoupon failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCouponsResponse> GetCoupons()
        {
            try
            {
                var allEntities = await _couponRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetCouponsResponse { CouponList = new List<CouponDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCoupons failed.", SystemErrorCode.SystemError);
            }
        }

        private GetCouponsResponse ConvertEntitiesToResponse(List<CouponEntity> allEntities)
        {
            var entities = allEntities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetCouponsResponse
            {
                CouponList = entities
            };
        }

        private CouponDTO ConvertFromEntity(CouponEntity x)
        {
            return new CouponDTO
            {
                CouponId = x.CouponId,
                Code = x.Code,
                CreatedBy = x.CreatedBy,
                Enabled = x.Enabled,
                ExpirationDate = x.ExpirationDate,
                MoneyValue = x.MoneyValue,
                UpdatedBy = x.UpdatedBy,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            };
        }

        private void ValidateRequest(CouponIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CouponId == 0)
            {
                throw new SystemBaseException("CouponId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(CouponDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.MoneyValue < 0.0)
            {
                throw new SystemBaseException("MoneyValue is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                throw new SystemBaseException("Code is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
