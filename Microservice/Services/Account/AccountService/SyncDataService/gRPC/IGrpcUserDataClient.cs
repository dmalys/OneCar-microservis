using UserService.Proto;

namespace AccountService.SyncDataService.gRPC
{
    public interface IGrpcUserDataClient
    {
        void NotifyDelete(NotifyDeleteAccountRequest request);

    }
}
