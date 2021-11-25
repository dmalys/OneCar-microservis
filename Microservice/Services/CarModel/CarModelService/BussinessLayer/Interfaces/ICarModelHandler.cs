using CarModelService.BusinessLayer.CarModel.Models;
using System.Threading.Tasks;

namespace CarModelService.BusinessLayer.CarImage.Interfaces
{
    public interface ICarModelHandler
    {
        Task<GetCarModelResponse> GetCarModel(GetCarModelRequest request);

        Task UpdateCarModel(UpdateCarModelRequest request);

        Task DeleteCarModel(DeleteCarModelRequest request);

        Task AddCarModel(AddCarModelRequest request);

        Task<GetCarModelsResponse> GetCarModels();
    }
}
