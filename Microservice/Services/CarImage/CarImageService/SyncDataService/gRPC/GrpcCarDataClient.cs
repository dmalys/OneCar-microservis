using CarService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace BrandService.SyncDataService.gRPC
{
    public class GrpcCarDataClient : IGrpcCarDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcCarDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void NotifyDelete(NotifyDeleteCarImageRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCar"]);
            var client = new GrpcCar.GrpcCarClient(channel);

            try
            {
                client.NotifyDeleteCarImageEntity(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
