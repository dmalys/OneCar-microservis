using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.BusinessLayer.User.Interfaces;
using UserService.BusinessLayer.User.Models;
using Swashbuckle.Swagger.Annotations;

namespace UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserHandler _userHandler;

        public UserController(ILogger<UserController> logger,
             IUserHandler userHandler)
        {
            _logger = logger;
            _userHandler = userHandler;
        }

        [HttpPost]
        [Route("GetUsers")]
        [SwaggerOperation(OperationId = "GetUsers")]
        [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
        public async Task<GetUsersResponse> GetUsers()
        {
            return await _userHandler.GetUsers();
        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        public async Task<GetUserResponse> GetDetails([FromBody] GetUserRequest request)
        {
            return await _userHandler.GetUser(request);
        }

        [HttpPost]
        [Route("RentCar")]
        [SwaggerOperation(OperationId = "RentCar")]
        public async Task RentCar([FromBody] RentCarRequest request)
        {
            await _userHandler.RentCar(request);
        }

        [HttpPost]
        [Route("UpdateUser")]
        [SwaggerOperation(OperationId = "UpdateUser")]
        public async Task UpdateUser([FromBody]UpdateUserRequest request)
        {
            await _userHandler.UpdateUser(request);
        }

        [HttpPost]
        [Route("AddUser")]
        [SwaggerOperation(OperationId = "AddUser")]
        public async Task AddCar([FromBody]AddUserRequest request)
        {
            await _userHandler.AddUser(request);
        }

        [HttpPost]
        [Route("DeleteUser")]
        [SwaggerOperation(OperationId = "DeleteUser")]
        public async Task DeleteUser([FromBody]DeleteUserRequest request)
        {
            await _userHandler.DeleteUser(request);
        }

        [HttpPost]
        [Route("AddCoupon")]
        [SwaggerOperation(OperationId = "AddCoupon")]
        public async Task AddCoupon(AddCouponMoneyValueRequest request)
        {
            await _userHandler.AddCouponMoneyValue(request);
        }
    }
}
