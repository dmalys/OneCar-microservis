using BrandService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace CarModelService.SyncDataService.gRPC
{
    public class GrpcBrandDataClient : IGrpcBrandDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcBrandDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckBrandExist(CheckBrandRequest brandRequest)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcBrand"]);
            var client = new GrpcBrand.GrpcBrandClient(channel);
            var request = new CheckBrandRequest() 
            { 
                BrandId = brandRequest.BrandId
            };

            try
            {
                var response = client.CheckBrandExists(request);
                return response.Exists == 2 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
