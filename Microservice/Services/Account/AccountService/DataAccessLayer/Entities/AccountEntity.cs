using LinqToDB.Mapping;
using System;

namespace AccountService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "accounts")]
    public class AccountEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "account_id")]
        public int AccountId { get; set; }

        [Column(Name = "account_type")]
        public string AccountType { get; set; }

        [Column(Name = "dicount")]
        public int Discount { get; set; }

        [Column(Name = "price_per_month")]
        public float PricePerMonth { get; set; }

        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "update_date")]
        public DateTime? UpdateDate { get; set; }
    }
}
