using BrandService.DataAccessLayer.Interfaces;
using BrandService.Proto;
using Grpc.Core;
using System.Threading.Tasks;

namespace CarModelService.SyncDataServices.gRPC
{
    public class GrpcBrandService : GrpcBrand.GrpcBrandBase
    {
        private readonly IBrandRepository _brandRepository;

        public GrpcBrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public override Task<CheckBrandResponse> CheckBrandExists(CheckBrandRequest request, ServerCallContext context)
        {
            var response = new CheckBrandResponse
            {
                Exists = _brandRepository.CheckBrandExists(request.BrandId).Result == true ? 2 : 1
            };
            return Task.FromResult(response);
        }
    }
}
