using System;

namespace TicketService.BusinessLayer.Ticket.Models
{
    public class AddTicketRequest
    {
        public int CarId { get; set; }

        public DateTime ExpirationDate { get; set; }
        
        public string CreatedBy { get; set; }
    }
}
