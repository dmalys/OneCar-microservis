using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.BusinessLayer.Login.Models
{
    public abstract class AccountDetailedRequest
    {
        public string AccountType { get; set; }

        public int Discount { get; set; }

        public float PricePerMonth { get; set; }
    }
}
