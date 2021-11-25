using CarImageService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace CarImageService.SyncDataService.gRPC
{
    public class GrpcCarImageDataClient : IGrpcCarImageDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcCarImageDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckCarImageExist(CheckCarImageRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCarImage"]);
            var client = new GrpcCarImage.GrpcCarImageClient(channel);
            var grpcRequest = new CheckCarImageRequest()
            {
                CarImageId = request.CarImageId
            };

            try
            {
                var response = client.CheckCarImageExists(grpcRequest);
                return response.Exists == 2 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
