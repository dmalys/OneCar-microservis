using AccountService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace UserService.SyncDataService.gRPC
{
    public class GrpcAccountDataClient : IGrpcAccountDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcAccountDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckAccountExist(CheckAccountRequest accountRequest)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcAccount"]);
            var client = new GrpcAccount.GrpcAccountClient(channel);
            var request = new CheckAccountRequest()
            {
                AccountId = accountRequest.AccountId
            };

            try
            {
                var response = client.CheckAccountExists(request);
                return response.Exists == 2 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
