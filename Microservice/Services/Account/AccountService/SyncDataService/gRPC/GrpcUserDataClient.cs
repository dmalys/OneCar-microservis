using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using UserService.Proto;

namespace AccountService.SyncDataService.gRPC
{
    public class GrpcUserDataClient : IGrpcUserDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcUserDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void NotifyDelete(NotifyDeleteAccountRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcUser"]);
            var client = new GrpcUser.GrpcUserClient(channel);

            try
            {
                client.NotifyDeleteAccountEntity(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
