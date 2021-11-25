using TicketService.Proto;

namespace CarModelService.SyncDataService.gRPC
{
    public interface IGrpcTicketDataClient
    {
        void NotifyDelete(NotifyDeleteTicketRequest request);

    }
}
