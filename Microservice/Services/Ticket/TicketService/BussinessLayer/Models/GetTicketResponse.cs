using Newtonsoft.Json;

namespace TicketService.BusinessLayer.Ticket.Models
{
    public class GetTicketResponse
    {
        [JsonProperty("ticketList")]
        public TicketDTO Ticket { get; set; }
    }
}
