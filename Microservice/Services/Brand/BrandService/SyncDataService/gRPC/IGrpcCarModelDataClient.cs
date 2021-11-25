using CarModelService.Proto;

namespace BrandService.SyncDataService.gRPC
{
    public interface IGrpcCarModelDataClient
    {
        void NotifyDelete(NotifyDeleteBrandRequest request);

    }
}
