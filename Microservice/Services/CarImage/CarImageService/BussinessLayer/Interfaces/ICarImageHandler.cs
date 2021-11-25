using Microsoft.AspNetCore.Http;
using CarImageService.BusinessLayer.CarImage.Models;
using System.Threading.Tasks;

namespace CarImageService.BusinessLayer.CarImage.Interfaces
{
    public interface ICarImageHandler
    {
        Task<GetCarImageResponse> GetCarImage(GetCarImageRequest request);

        Task UpdateCarImage(UpdateCarImageRequest request);

        Task DeleteCarImage(DeleteCarImageRequest request);

        Task AddCarImage(AddCarImageRequest request);

        Task<GetCarImagesResponse> GetCarImages();
    }
}
