using UserService.DataAccessLayer.Interfaces;
using UserService.Proto;
using Grpc.Core;
using System.Threading.Tasks;

namespace UserService.SyncDataServices.gRPC
{
    public class GrpcUserService : GrpcUser.GrpcUserBase
    {
        private readonly IUserRepository _userRepository;

        public GrpcUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override Task<NotifyDeleteAccountResponse> NotifyDeleteAccountEntity(NotifyDeleteAccountRequest request, ServerCallContext context)
        {
            _userRepository.DeleteAsyncByAccountId(request.AccountId);

            return Task.FromResult(new NotifyDeleteAccountResponse());
        }
    }
}
