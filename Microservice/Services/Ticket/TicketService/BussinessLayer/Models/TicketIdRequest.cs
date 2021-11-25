using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.BusinessLayer.Ticket.Models
{
    public abstract class TicketIdRequest
    {
        [JsonProperty("ticketId")]
        public int TicketId { get; set; }
    }
}
