using AccountService.DataAccessLayer.Interfaces;
using AccountService.Proto;
using Grpc.Core;
using System.Threading.Tasks;

namespace AccountService.SyncDataServices.gRPC
{
    public class GrpcAccountService : GrpcAccount.GrpcAccountBase
    {
        private readonly IAccountRepository _accountRepository;

        public GrpcAccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public override Task<CheckAccountResponse> CheckAccountExists(CheckAccountRequest request, ServerCallContext context)
        {
            var response = new CheckAccountResponse
            {
                Exists = _accountRepository.CheckAccountExists(request.AccountId).Result == true ? 2 : 1
            };
            return Task.FromResult(response);
        }
    }
}
