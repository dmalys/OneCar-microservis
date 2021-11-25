using LinqToDB.Mapping;
using System;

namespace CouponService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "coupons")]
    public class CouponEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "coupon_id")]
        public int CouponId { get; set; }
        
        [Column(Name = "code")]
        public string Code { get; set; }

        [Column(Name = "money_value")]
        public float MoneyValue { get; set; }

        [Column(Name = "enabled")]
        public bool Enabled { get; set; }

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
