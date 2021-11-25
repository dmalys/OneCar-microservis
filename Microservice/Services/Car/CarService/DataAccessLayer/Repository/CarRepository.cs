using CarService.DataAccessLayer.Entities;
using CarService.DataAccessLayer.Interfaces;
using LinqToDB;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.DataAccessLayer.Repository
{
    public class CarRepository : BaseRepository<CarEntity, int>, ICarRepository
    {
        public CarRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<CarEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CarId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<CarEntity>().DeleteAsync(x => x.CarId == id);
            }
        }

        public async Task<bool> CheckCarExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CarId == identity))?.CarId > 0;
        }

        public async Task<IList<int>> DeleteBulkByCarModelIdAsync(IList<int> carModelIds)
        {
            using (var db = CreateConnection(_configuration))
            {
                var idsList = await db.GetTable<CarEntity>().Where(x => carModelIds.Contains(x.CarModelId)).Select(x => x.CarId).ToListAsync();
                await db.GetTable<CarEntity>().DeleteAsync(x => carModelIds.Contains(x.CarModelId));

                return idsList;
            }
        }

        public async Task UpdateByCarImageIdToNullAsync(int carImageId)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<CarEntity>().Where(x => x.CarImageId == carImageId)
                    .Set(x => x.CarImageId, x => null)
                    .UpdateAsync();
            }
        }
    }
}
