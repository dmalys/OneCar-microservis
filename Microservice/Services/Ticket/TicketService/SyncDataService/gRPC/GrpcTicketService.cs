using Grpc.Core;
using System.Threading.Tasks;
using TicketService.DataAccessLayer.Interfaces;
using TicketService.Proto;

namespace CarModelService.SyncDataServices.gRPC
{
    public class GrpcTicketService : GrpcTicket.GrpcTicketBase
    {
        private readonly ITicketRepository _ticketRepository;

        public GrpcTicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public override Task<NotifyDeleteTicketResponse> NotifyDeleteTicketEntity(NotifyDeleteTicketRequest request, ServerCallContext context)
        {
            _ticketRepository.DeleteBulkByCarIdAsync(request.CarIds);

            return Task.FromResult(new NotifyDeleteTicketResponse());
        }
    }
}
