using CarService.Proto;

namespace CarModelService.SyncDataService.gRPC
{
    public interface IGrpcCarDataClient
    {
        void NotifyDelete(NotifyDeleteCarModelRequest request);
    }
}
