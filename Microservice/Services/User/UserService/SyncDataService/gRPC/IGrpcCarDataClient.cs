using CarService.Proto;

namespace UserService.SyncDataService.gRPC
{
    public interface IGrpcCarDataClient
    {
        CarDTO GetCarData(GetCarDataRequest request);
    }
}
