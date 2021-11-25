using CarModelService.DataAccessLayer.Entities;
using CarModelService.DataAccessLayer.Interfaces;
using LinqToDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.DataAccessLayer.Repository
{
    public class CarModelsRepository : BaseRepository<CarModelEntity, int>, ICarModelsRepository
    {
        public CarModelsRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<CarModelEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CarModelId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = CreateConnection(_configuration))
            {
                await db.GetTable<CarModelEntity>().DeleteAsync(x => x.CarModelId == id);
            }
        }

        public async Task<bool> CheckCarModelExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CarModelId == identity))?.CarModelId > 0;
        }

        public async Task<IList<int>> DeleteBulkByBrandIdAsync(int brandId)
        {
            using (var db = CreateConnection(_configuration))
            {
                var idsList = await db.GetTable<CarModelEntity>().Where(x => x.BrandId == brandId).Select(x => x.CarModelId).ToListAsync();
                await db.GetTable<CarModelEntity>().DeleteAsync(x => x.BrandId == brandId);

                return idsList;
            }
        }
    }
}
