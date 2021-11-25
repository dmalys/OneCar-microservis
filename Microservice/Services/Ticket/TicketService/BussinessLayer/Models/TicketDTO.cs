using System;

namespace TicketService.BusinessLayer.Ticket.Models
{
    public class TicketDTO
    {
        public int TicketId { get; set; }

        public int CarId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
