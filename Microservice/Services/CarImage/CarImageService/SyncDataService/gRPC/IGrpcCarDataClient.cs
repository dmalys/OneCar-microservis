using CarService.Proto;

namespace BrandService.SyncDataService.gRPC
{
    public interface IGrpcCarDataClient
    {
        void NotifyDelete(NotifyDeleteCarImageRequest request);

    }
}
