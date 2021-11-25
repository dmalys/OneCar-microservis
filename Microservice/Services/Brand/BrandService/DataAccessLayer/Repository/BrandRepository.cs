using BrandService.DataAccessLayer.Entities;
using BrandService.DataAccessLayer.Interfaces;
using LinqToDB;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BrandService.DataAccessLayer.Repository
{
    public class BrandRepository : BaseRepository<BrandEntity, int>, IBrandRepository
    {
        public BrandRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<BrandEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.BrandId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<BrandEntity>().DeleteAsync(x => x.BrandId == id);
            }
        }

        public async Task<bool> CheckBrandExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.BrandId == identity))?.BrandId > 0;
        }
    }
}
