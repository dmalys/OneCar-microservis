using LinqToDB.Mapping;
using System;

namespace TicketService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "tickets")]
    public class TicketEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "ticket_id")]
        public int TicketId { get; set; }

        [Column(Name = "car_id")]
        public int CarId { get; set; }

        [Column(Name = "expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column(Name = "created_by")]
        public string CreatedBy { get; set; }

        [Column(Name = "updated_by")]
        public string UpdatedBy { get; set; }
    }
}
