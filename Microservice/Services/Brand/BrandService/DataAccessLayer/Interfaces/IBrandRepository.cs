using BrandService.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrandService.DataAccessLayer.Interfaces
{
    public interface IBrandRepository
    {
        Task<BrandEntity> GetAsync(int identity);
        Task<int> Insert(BrandEntity brand);
        Task<int> Update(BrandEntity brand);

        Task<List<BrandEntity>> GetAll();

        Task<bool> CheckBrandExists(int identity);
        Task DeleteAsync(int id);
    }
}
