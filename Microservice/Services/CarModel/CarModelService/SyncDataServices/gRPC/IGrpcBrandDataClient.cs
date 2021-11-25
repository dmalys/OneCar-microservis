using BrandService.Proto;

namespace CarModelService.SyncDataService.gRPC
{
    public interface IGrpcBrandDataClient
    {
        bool CheckBrandExist(CheckBrandRequest brandRequest);
    }
}
