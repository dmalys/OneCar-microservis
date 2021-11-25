using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using TicketService.Proto;

namespace CarModelService.SyncDataService.gRPC
{
    public class GrpcTicketDataClient : IGrpcTicketDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcTicketDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void NotifyDelete(NotifyDeleteTicketRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcTicket"]);
            var client = new GrpcTicket.GrpcTicketClient(channel);

            try
            {
                client.NotifyDeleteTicketEntity(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
