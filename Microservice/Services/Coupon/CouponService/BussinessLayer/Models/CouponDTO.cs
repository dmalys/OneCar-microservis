using System;

namespace CouponService.BusinessLayer.User.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        
        public string Code { get; set; }

        public float MoneyValue { get; set; }

        public bool Enabled { get; set; }

        public DateTime ExpirationDate { get; set; }
        
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
