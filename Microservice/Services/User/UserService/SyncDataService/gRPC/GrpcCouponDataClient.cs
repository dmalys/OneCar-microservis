using CouponService.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using UserService.BusinessLayer.ErrorHandling;

namespace UserService.SyncDataService.gRPC
{
    public class GrpcCouponDataClient : IGrpcCouponDataClient
    {
        private readonly IConfiguration _configuration;

        public GrpcCouponDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CouponDTO GetCouponData(GetCouponDataRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCoupon"]);
            var client = new GrpcCoupon.GrpcCouponClient(channel);
            var grpcRequest = new GetCouponDataRequest()
            {
                CouponCode = request.CouponCode
            };

            try
            {
                var response = client.GetCouponData(grpcRequest);

                return new CouponDTO
                {
                    CouponId = response.CouponId,
                    ExpirationDate = response.ExpirationDate.ToDateTime(),
                    MoneyValue = response.MoneyValue
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCouponData error", SystemErrorCode.SystemError);
            }
        }

        public void NotifyDeleteCouponEntity(NotifyDeleteCouponRequest request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCoupon"]);
            var client = new GrpcCoupon.GrpcCouponClient(channel);
            var grpcRequest = new NotifyDeleteCouponRequest()
            {
                CouponId = request.CouponId
            };

            try
            {
                client.NotifyDeleteCouponEntity(grpcRequest);
            }
            catch (Exception)
            {
                throw new SystemBaseException("NotifyDeleteCouponEntity error", SystemErrorCode.SystemError);
            }
        }
    }
}
