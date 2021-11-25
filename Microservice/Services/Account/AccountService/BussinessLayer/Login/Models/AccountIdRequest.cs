using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.BusinessLayer.Login.Models
{
    public abstract class AccountIdRequest
    {
        public int AccountId { get; set; }
    }
}
