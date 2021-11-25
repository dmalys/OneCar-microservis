using CarImageService.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarImageService.DataAccessLayer.Interfaces
{
    public interface ICarImageRepository
    {
        Task<CarImageEntity> GetAsync(int identity);
        Task<int> Insert(CarImageEntity carImage);
        Task<int> Update( CarImageEntity carImage);

        Task<List<CarImageEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckCarImageExists(int identity);

    }
}
