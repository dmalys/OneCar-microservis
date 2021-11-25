using CarModelService.SyncDataService.gRPC;
using CarService.DataAccessLayer.Interfaces;
using CarService.Proto;
using Grpc.Core;
using System.Threading.Tasks;
using TicketService.Proto;

namespace CarModelService.SyncDataServices.gRPC
{
    public class GrpcCarService : GrpcCar.GrpcCarBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IGrpcTicketDataClient _grpcTicketDataClient;

        public GrpcCarService(ICarRepository carRepository, IGrpcTicketDataClient grpcTicketDataClient)
        {
            _carRepository = carRepository;
            _grpcTicketDataClient = grpcTicketDataClient;
        }

        public override Task<CheckCarResponse> CheckCarExists(CheckCarRequest request, ServerCallContext context)
        {
            var response = new CheckCarResponse
            {
                Exists = _carRepository.CheckCarExists(request.CarId).Result ? 2 : 1
            };

            return Task.FromResult(response);
        }

        public override Task<GetCarDataResponse> GetCarData(GetCarDataRequest request, ServerCallContext context)
        {
            var carEntity = _carRepository.GetAsync(request.CarId).Result;

            var response = new GetCarDataResponse
            {
                PricePerHour = carEntity.PricePerHour
            };

            return Task.FromResult(response);
        }

        public override Task<NotifyDeleteCarModelResponse> NotifyDeleteCarModelEntity(NotifyDeleteCarModelRequest request, ServerCallContext context)
        {
            var idsList = _carRepository.DeleteBulkByCarModelIdAsync(request.CarModelIds).Result;

            var ticketRequest = new NotifyDeleteTicketRequest();
            ticketRequest.CarIds.AddRange(idsList);

            _grpcTicketDataClient.NotifyDelete(ticketRequest);
            //TODO: notify dlete should run background task to remove those entries

            return Task.FromResult(new NotifyDeleteCarModelResponse());
        }

        public override Task<NotifyDeleteCarImageResponse> NotifyDeleteCarImageEntity(NotifyDeleteCarImageRequest request, ServerCallContext context)
        {
            _carRepository.UpdateByCarImageIdToNullAsync(request.CarImageId);

            return Task.FromResult(new NotifyDeleteCarImageResponse());
        }
    }
}
