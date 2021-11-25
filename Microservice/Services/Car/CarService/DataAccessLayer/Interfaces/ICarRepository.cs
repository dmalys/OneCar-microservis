using CarService.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarService.DataAccessLayer.Interfaces
{
    public interface ICarRepository
    {
        Task<CarEntity> GetAsync(int identity);
        Task<int> Insert(CarEntity car);
        Task<int> Update(CarEntity car);

        Task<List<CarEntity>> GetAll();

        Task DeleteAsync(int id);

        Task<bool> CheckCarExists(int identity);
        Task<IList<int>> DeleteBulkByCarModelIdAsync(IList<int> carModelIds);
        Task UpdateByCarImageIdToNullAsync(int carImageId);

    }
}
