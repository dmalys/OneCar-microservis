using LinqToDB;
using CarImageService.DataAccessLayer.Entities;
using CarImageService.DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CarImageService.DataAccessLayer.Repositories
{
    public class CarImageRepository : BaseRepository<CarImageEntity, int>, ICarImageRepository
    {
        public CarImageRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<CarImageEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CarImageId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<CarImageEntity>().DeleteAsync(x => x.CarImageId == id);
            }
        }

        public async Task<bool> CheckCarImageExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CarImageId == identity))?.CarImageId > 0;
        }
    }
}