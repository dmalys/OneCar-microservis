using CarModelService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace BrandService.SyncDataService.gRPC
{
    public class GrpcCarModelDataClient : IGrpcCarModelDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcCarModelDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void NotifyDelete(NotifyDeleteBrandRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCarModel"]);
            var client = new GrpcCarModel.GrpcCarModelClient(channel);

            try
            {
                client.NotifyDeleteBrandEntity(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
