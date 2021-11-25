using Newtonsoft.Json;

namespace UserService.BusinessLayer.User.Models
{
    public class GetUserResponse
    {
        [JsonProperty("user")]
        public UserDTO User { get; set; }
    }
}
