using Newtonsoft.Json;
using System.Collections.Generic;

namespace TicketService.BusinessLayer.Ticket.Models
{
    public class GetTicketsResponse
    {
        [JsonProperty("ticketList")]
        public IList<TicketDTO> TicketList { get; set; }
    }
}
