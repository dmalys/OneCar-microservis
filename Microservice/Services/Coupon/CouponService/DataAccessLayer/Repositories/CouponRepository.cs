using LinqToDB;
using CouponService.DataAccessLayer.Entities;
using CouponService.DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CouponService.DataAccessLayer.Repositories
{
    public class CouponRepository : BaseRepository<CouponEntity, int>, ICouponRepository
    {
        public CouponRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<CouponEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CouponId == identity);
        }

        public async Task<CouponEntity> GetAsyncByCode(string couponCode)
        {
            return await GetSingleOrDefaultAsync(u => u.Code == couponCode);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<CouponEntity>().DeleteAsync(x => x.CouponId == id);
            }
        }

        public async Task<bool> CheckCouponExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CouponId == identity))?.CouponId > 0;
        }
    }
}
