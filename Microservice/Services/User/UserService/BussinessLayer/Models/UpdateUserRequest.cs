namespace UserService.BusinessLayer.User.Models
{
    public class UpdateUserRequest : UserDetailedRequest
    {
        public int UserId { get; set; }
    }
}
