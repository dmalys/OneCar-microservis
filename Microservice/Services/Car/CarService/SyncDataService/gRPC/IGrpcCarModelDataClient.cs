using CarModelService.Proto;

namespace CarModelService.SyncDataService.gRPC
{
    public interface IGrpcCarModelDataClient
    {
        public bool CheckCarModelExist(CheckCarModelRequest request);
    }
}
