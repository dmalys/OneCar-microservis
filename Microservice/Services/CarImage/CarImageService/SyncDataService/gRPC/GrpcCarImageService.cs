using CarImageService.DataAccessLayer.Interfaces;
using CarImageService.Proto;
using Grpc.Core;
using System.Threading.Tasks;

namespace CarModelService.SyncDataServices.gRPC
{
    public class GrpcCarImageService : GrpcCarImage.GrpcCarImageBase
    {
        private readonly ICarImageRepository _carImageRepository;

        public GrpcCarImageService(ICarImageRepository carImageRepository)
        {
            _carImageRepository = carImageRepository;
        }

        public override Task<CheckCarImageResponse> CheckCarImageExists(CheckCarImageRequest request, ServerCallContext context)
        {
            var response = new CheckCarImageResponse
            {
                Exists = _carImageRepository.CheckCarImageExists(request.CarImageId).Result == true ? 2 : 1
            };
            return Task.FromResult(response);
        }
    }
}
