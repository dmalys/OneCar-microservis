using CarService.Proto;

namespace BrandService.SyncDataService.gRPC
{
    public interface IGrpcCarDataClient
    {
        bool CheckCarExist(CheckCarRequest request);
    }
}
