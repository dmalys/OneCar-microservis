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

        public bool CheckCarExist(CheckCarRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCar"]);
            var client = new GrpcCar.GrpcCarClient(channel);
            var grpcRequest = new CheckCarRequest()
            {
                CarId = request.CarId
            };

            try
            {
                var response = client.CheckCarExists(grpcRequest);
                return response.Exists == 2 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
