using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarService.BusinessLayer.Car.Interfaces;
using CarService.BusinessLayer.Car.Models;
using Swashbuckle.Swagger.Annotations;

namespace CarService.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarHandler _getCarHandler;

        public CarController(
            ILogger<CarController> logger,
            ICarHandler getCarHandler
            )
        {
            _logger = logger;

            _getCarHandler = getCarHandler;
        }

        [HttpPost]
        [Route("GetCars")]
        [SwaggerOperation(OperationId = "GetCars")]
        [ProducesResponseType(typeof(GetCarsResponse), StatusCodes.Status200OK)]
        public async Task<GetCarsResponse> GetCars()
        {
            return await _getCarHandler.GetCars();

        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetCarResponse), StatusCodes.Status200OK)]
        public async Task<GetCarResponse> GetDetails([FromBody] GetCarRequest request)
        {
            return await _getCarHandler.GetCar(request);
        }

        [HttpPost]
        [Route("UpdateCar")]
        [SwaggerOperation(OperationId = "UpdateCar")]
        public async Task UpdateCar([FromBody] UpdateCarRequest request)
        {
            await _getCarHandler.UpdateCar(request);
        }

        [HttpPost]
        [Route("AddCar")]
        [SwaggerOperation(OperationId = "AddCar")]
        public async Task AddCar([FromBody] AddCarRequest request)
        {
            await _getCarHandler.AddCar(request);
        }

        [HttpPost]
        [Route("DeleteCar")]
        [SwaggerOperation(OperationId = "DeleteCar")]
        public async Task DeleteCar([FromBody] DeleteCarRequest request)
        {
            await _getCarHandler.DeleteCar(request);
        }

        //TODO: extend
        ////public async Task<IList<string>> GetLocalizations()
        ////{
        ////    throw new NotImplementedException();

        ////}
        ///
          ////public async Task ActivateCar()
        ////{
        ////    var request = new UpdateCarRequest
        ////    {

        ////    }
        ////    await _getCarHandler.UpdateCar(request)

        ////}
    }
}
