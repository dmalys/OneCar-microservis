using CarModelService.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.DataAccessLayer.Interfaces
{
    public interface ICarModelsRepository
    {
        Task<CarModelEntity> GetAsync(int identity);
        Task<int> Insert(CarModelEntity carModel);
        Task<int> Update(CarModelEntity carModel);

        Task<List<CarModelEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckCarModelExists(int identity);
        Task<IList<int>> DeleteBulkByBrandIdAsync(int brandId);

    }
}
