using System;

namespace AccountService.BusinessLayer.Login.Models
{
    public class AccountDTO
    {
        public int AccountId { get; set; }

        public string AccountType { get; set; }

        public int Discount { get; set; }

        public float PricePerMonth { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        //public string CreatedBy { get; set; } // moze tu nie byc potrzebne

        //public string UpdatedBy { get; set; } // moze tu nie byc potrzebne
    }
}
