using CarImageService.Proto;

namespace CarImageService.SyncDataService.gRPC
{
    public interface IGrpcCarImageDataClient
    {
        public bool CheckCarImageExist(CheckCarImageRequest request);
    }
}
