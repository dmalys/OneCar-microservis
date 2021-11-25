using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.BusinessLayer.Login.Models
{
    public class GetAccountResponse
    {
        [JsonProperty("account")]
        public AccountDTO Account { get; set; }
    }
}
