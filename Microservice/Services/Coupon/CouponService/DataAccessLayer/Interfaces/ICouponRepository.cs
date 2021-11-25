using CouponService.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponService.DataAccessLayer.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponEntity> GetAsync(int identity);
        Task<int> Insert(CouponEntity coupon);
        Task<int> Update(CouponEntity coupon);

        Task<List<CouponEntity>> GetAll();

        Task DeleteAsync(int id);


        Task<bool> CheckCouponExists(int identity);
        Task<CouponEntity> GetAsyncByCode(string couponCode);

    }
}
