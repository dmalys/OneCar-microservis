using UserService.BusinessLayer.User.Models;
using System.Threading.Tasks;

namespace UserService.BusinessLayer.User.Interfaces
{
    public interface IUserHandler
    {
        Task<GetUserResponse> GetUser(GetUserRequest request);

        Task UpdateUser(UpdateUserRequest request);

        Task DeleteUser(DeleteUserRequest request);

        Task AddUser(AddUserRequest request);

        Task<GetUsersResponse> GetUsers(); //Only for admins

        Task AddCouponMoneyValue(AddCouponMoneyValueRequest request);
        Task RentCar(RentCarRequest request);
    }
}
