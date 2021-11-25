using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.BusinessLayer.Login.Models
{
    public class GetAccountsResponse
    {
        [JsonProperty("accountList")]
        public List<AccountDTO> AccountList { get; set; }
    }
}
