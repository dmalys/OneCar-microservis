using CarModelService.DataAccessLayer.Interfaces;
using CarModelService.Proto;
using CarModelService.SyncDataService.gRPC;
using CarService.Proto;
using Grpc.Core;
using System.Threading.Tasks;

namespace CarModelService.SyncDataServices.gRPC
{
    public class GrpcCarModelService : GrpcCarModel.GrpcCarModelBase
    {
        private readonly ICarModelsRepository _carModelRepository;
        private readonly IGrpcCarDataClient _grpcCarDataClient;

        public GrpcCarModelService(ICarModelsRepository carModelRepository, IGrpcCarDataClient grpcCarDataClient)
        {
            _carModelRepository = carModelRepository;
            _grpcCarDataClient = grpcCarDataClient;
        }

        public override Task<CheckCarModelResponse> CheckCarModelExists(CheckCarModelRequest request, ServerCallContext context)
        {
            var response = new CheckCarModelResponse
            {
                Exists = _carModelRepository.CheckCarModelExists(request.CarModelId).Result ? 2 : 1
            };

            return Task.FromResult(response);
        }

        public override Task<NotifyDeleteBrandResponse> NotifyDeleteBrandEntity(NotifyDeleteBrandRequest request, ServerCallContext context)
        {
            var idsList = _carModelRepository.DeleteBulkByBrandIdAsync(request.BrandId).Result;

            var carRequest = new NotifyDeleteCarModelRequest();
            carRequest.CarModelIds.AddRange(idsList);

            _grpcCarDataClient.NotifyDelete(carRequest);

            return Task.FromResult(new NotifyDeleteBrandResponse());
        }
    }
}
