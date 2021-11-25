using CarModelService.BusinessLayer.CarImage.Interfaces;
using CarModelService.BusinessLayer.CarModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.Controllers
{
    [Route("api/carmodel")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly ICarModelHandler _carModelHandler;

        public CarModelController(ILogger<CarModelController> logger,
             ICarModelHandler carModelHandler)
        {
            _logger = logger;
            _carModelHandler = carModelHandler;
        }

        [HttpGet]
        [Route("GetCarModels")]
        [SwaggerOperation(OperationId = "GetCarModels")]
        [ProducesResponseType(typeof(GetCarModelsResponse), StatusCodes.Status200OK)]
        public async Task<GetCarModelsResponse> GetCarModels()
        {
            return await _carModelHandler.GetCarModels();
        }

        [HttpGet]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetCarModelResponse), StatusCodes.Status200OK)]
        public async Task<GetCarModelResponse> GetDetails([FromBody] GetCarModelRequest request)
        {
            return await _carModelHandler.GetCarModel(request);
        }

        [HttpPost]
        [Route("UpdateCarModel")]
        [SwaggerOperation(OperationId = "UpdateCarModel")]
        public async Task UpdateCarModel([FromBody] UpdateCarModelRequest request)
        {
            await _carModelHandler.UpdateCarModel(request);
        }

        [HttpPost]
        [Route("AddCarModel")]
        [SwaggerOperation(OperationId = "AddCarModel")]
        public async Task AddCarModel([FromBody] AddCarModelRequest request)
        {
            await _carModelHandler.AddCarModel(request);
        }

        [HttpPost]
        [Route("DeleteCarModel")]
        [SwaggerOperation(OperationId = "DeleteCarModel")]
        public async Task DeleteCarModel([FromBody] DeleteCarModelRequest request)
        {
            await _carModelHandler.DeleteCarModel(request);
        }
    }
}
