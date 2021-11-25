using AccountService.Proto;

namespace UserService.SyncDataService.gRPC
{
    public interface IGrpcAccountDataClient
    {
        bool CheckAccountExist(CheckAccountRequest accountRequest);
    }
}
