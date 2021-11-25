using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.BusinessLayer.Login.Models
{
    public class UpdateAccountRequest : AccountDetailedRequest
    {
        public int AccountId { get; set; }
    }
}
