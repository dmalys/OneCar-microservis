namespace UserService.BusinessLayer.User.Models
{
    public class AddCouponMoneyValueRequest
    {
        public string CouponCode { get; set; }

        public int UserId { get; set; }
    }
}
