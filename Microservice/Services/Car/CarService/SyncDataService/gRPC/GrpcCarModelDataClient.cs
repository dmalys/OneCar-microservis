using CarModelService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace CarModelService.SyncDataService.gRPC
{
    public class GrpcCarModelDataClient : IGrpcCarModelDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcCarModelDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckCarModelExist(CheckCarModelRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCarModel"]);
            var client = new GrpcCarModel.GrpcCarModelClient(channel);
            var grpcRequest = new CheckCarModelRequest()
            {
                CarModelId = request.CarModelId
            };

            try
            {
                var response = client.CheckCarModelExists(grpcRequest);
                return response.Exists == 2 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
