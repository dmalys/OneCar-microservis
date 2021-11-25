using BrandService.BusinessLayer.Brand.Models;
using System.Threading.Tasks;

namespace BrandService.BusinessLayer.Brand.Implementation
{
    public interface IBrandHandler
    {
        Task<GetBrandResponse> GetBrand(GetBrandRequest request);

        Task UpdateBrand(UpdateBrandRequest request);

        Task DeleteBrand(DeleteBrandRequest request);

        Task AddBrand(AddBrandRequest request);

        Task<GetBrandsResponse> GetBrands();
    }
}
