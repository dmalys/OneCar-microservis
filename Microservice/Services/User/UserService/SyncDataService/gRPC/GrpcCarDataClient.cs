using CarService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using UserService.BusinessLayer.ErrorHandling;

namespace UserService.SyncDataService.gRPC
{
    public class GrpcCarDataClient : IGrpcCarDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcCarDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CarDTO GetCarData(GetCarDataRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCar"]);
            var client = new GrpcCar.GrpcCarClient(channel);
            var grpcRequest = new GetCarDataRequest()
            {
                CarId = request.CarId
            };

            try
            {
                var response = client.GetCarData(grpcRequest);

                return new CarDTO
                {
                    PricePerHour = response.PricePerHour
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCarData error", SystemErrorCode.SystemError);
            }
        }
    }
}
