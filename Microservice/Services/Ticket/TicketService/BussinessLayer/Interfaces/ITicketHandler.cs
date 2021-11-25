using TicketService.BusinessLayer.Ticket.Models;
using System.Threading.Tasks;

namespace TicketService.BusinessLayer.Ticket.Interfaces
{
    public interface ITicketHandler
    {
        Task<GetTicketResponse> GetTicket(GetTicketRequest request);
        
        Task DeleteTicket(DeleteTicketRequest request);

        Task AddTicket(AddTicketRequest request);

        Task<GetTicketsResponse> GetTickets(); //Only for admins
    }
}
