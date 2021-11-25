using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.BusinessLayer.User.Models
{
    public abstract class UserIdRequest
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
