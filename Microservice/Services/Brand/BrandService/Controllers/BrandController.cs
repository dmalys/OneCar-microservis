using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BrandService.BusinessLayer.Brand.Implementation;
using BrandService.BusinessLayer.Brand.Models;
using Swashbuckle.Swagger.Annotations;

namespace BrandService.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandHandler _brandHandler;

        public BrandController(ILogger<BrandController> logger,
             IBrandHandler brandHandler)
        {
            _logger = logger;
            _brandHandler = brandHandler;
        }

        [HttpGet]
        [Route("GetBrands")]
        [SwaggerOperation(OperationId = "GetBrands")]
        [ProducesResponseType(typeof(GetBrandsResponse), StatusCodes.Status200OK)]
        public async Task<GetBrandsResponse> GetBrands()
        {
            return await _brandHandler.GetBrands();

        }

        [HttpGet]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
        public async Task<GetBrandResponse> GetDetails([FromBody] GetBrandRequest request)
        {
            return await _brandHandler.GetBrand(request);
        }

        [HttpPost]
        [Route("UpdateBrand")]
        [SwaggerOperation(OperationId = "UpdateBrand")]
        public async Task UpdateBrand([FromBody] UpdateBrandRequest request)
        {
            await _brandHandler.UpdateBrand(request);
        }

        [HttpPost]
        [Route("AddBrand")]
        [SwaggerOperation(OperationId = "AddBrand")]
        public async Task AddBrand([FromBody] AddBrandRequest request)
        {
            await _brandHandler.AddBrand(request);
        }

        [HttpPost]
        [Route("DeleteBrand")]
        [SwaggerOperation(OperationId = "DeleteBrand")]
        public async Task DeleteBrand([FromBody] DeleteBrandRequest request)
        {
            await _brandHandler.DeleteBrand(request);
        }
    }
}
