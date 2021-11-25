using CarImageService.Proto;
using CarImageService.SyncDataService.gRPC;
using CarModelService.Proto;
using CarModelService.SyncDataService.gRPC;
using CarService.BusinessLayer.Car.Interfaces;
using CarService.BusinessLayer.Car.Models;
using CarService.BusinessLayer.ErrorHandling;
using CarService.DataAccessLayer.Entities;
using CarService.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Proto;

namespace CarService.BusinessLayer.Car.Implementation
{
    public class CarHandler : ICarHandler
    {
        private readonly ICarRepository _carRepository;
        private readonly IGrpcTicketDataClient _grpcTicketDataClient;
        private readonly IGrpcCarImageDataClient _grpcCarImageDataClient;
        private readonly IGrpcCarModelDataClient _grpcCarModelDataClient; 

        public CarHandler(
            ICarRepository carRepository,
            IGrpcTicketDataClient grpcTicketDataClient,
            IGrpcCarImageDataClient grpcCarImageDataClient,
            IGrpcCarModelDataClient grpcCarModelDataClient
            )
        {
            _carRepository = carRepository;
            _grpcTicketDataClient = grpcTicketDataClient;
            _grpcCarImageDataClient = grpcCarImageDataClient;
            _grpcCarModelDataClient = grpcCarModelDataClient;
        }

        public async Task<GetCarsResponse> GetCars()
        {
            try
            {
                var allEntities = await _carRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetCarsResponse { CarList = new List<CarDTO>() };
                }

                return ConvertEntities(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCar failed.", SystemErrorCode.SystemError);
            }           
        }

        public async Task AddCar(AddCarRequest request)
        {
            ValidateRequest(request);

            if(request.CarImageId != null && request.CarImageId > 0)
            {
                var checkCarImageRequest = new CheckCarImageRequest
                {
                    CarImageId = request.CarImageId.Value
                };
                var grpcImageResult = _grpcCarImageDataClient.CheckCarImageExist(checkCarImageRequest);

                if (!grpcImageResult)
                {
                    throw new SystemBaseException("CarImage entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            if (request.CarModelId > 0)
            {
                var checkCarModelRequest = new CheckCarModelRequest
                {
                    CarModelId = request.CarModelId
                };
                var grpcModelResult = _grpcCarModelDataClient.CheckCarModelExist(checkCarModelRequest);

                if (!grpcModelResult)
                {
                    throw new SystemBaseException("CarModel entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            var carEntity = new CarEntity();
            carEntity.CreateDate = DateTime.UtcNow;
            carEntity.CarImageId = request.CarImageId != null ? request.CarImageId : null;
            carEntity.CarModelId = request.CarModelId;
            carEntity.CreatedBy = request.CreatedBy;
            carEntity.Localization = request.Localization;
            carEntity.Mileage = request.Mileage;
            carEntity.PricePerHour = request.PricePerHour;
            carEntity.ProductionDate = request.ProductionDate;

            try
            {
                await _carRepository.Insert(carEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCar failed.", SystemErrorCode.SystemError);
            }
        }
        
        public async Task DeleteCar(DeleteCarRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _carRepository.CheckCarExists(request.CarId))
                {
                    await _carRepository.DeleteAsync(request.CarId);

                    //notify delete
                    var ticketRequest = new NotifyDeleteTicketRequest();
                    ticketRequest.CarIds.Add(request.CarId);

                    _grpcTicketDataClient.NotifyDelete(ticketRequest);
                }                    
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteCar failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateCar(UpdateCarRequest request)
        {
            ValidateRequest(request);
            if(request.CarId == 0)
            {
                throw new SystemBaseException("CarId is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.CarImageId != null && request.CarImageId > 0)
            {
                var checkCarImageRequest = new CheckCarImageRequest
                {
                    CarImageId = request.CarImageId.Value
                };
                var grpcResult = _grpcCarImageDataClient.CheckCarImageExist(checkCarImageRequest);

                if (!grpcResult)
                {
                    throw new SystemBaseException("CarImage entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            if (request.CarModelId > 0)
            {
                var checkCarModelRequest = new CheckCarModelRequest
                {
                    CarModelId = request.CarModelId
                };
                var grpcModelResult = _grpcCarModelDataClient.CheckCarModelExist(checkCarModelRequest);

                if (!grpcModelResult)
                {
                    throw new SystemBaseException("CarModel entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            try
            {
                if(await _carRepository.CheckCarExists(request.CarId))
                {
                    var carEntity = await _carRepository.GetAsync(request.CarId);
                    carEntity.UpdateDate = DateTime.UtcNow;
                    carEntity.CarImageId = request.CarImageId != null ? request.CarImageId : null;
                    carEntity.CarModelId = request.CarModelId;
                    carEntity.UpdatedBy = request.UpdatedBy;
                    carEntity.Localization = request.Localization;
                    carEntity.Mileage = request.Mileage;
                    carEntity.PricePerHour = request.PricePerHour;
                    carEntity.ProductionDate = request.ProductionDate;

                    await _carRepository.Update(carEntity);
                }
                else
                {
                    throw new SystemBaseException("Entity not found.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (SystemBaseException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateCar failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCarResponse> GetCar(GetCarRequest request)
        {
            ValidateRequest(request);

            try
            {
                var carEntity = await _carRepository.GetAsync(request.CarId);

                if(carEntity == null)
                {
                    return new GetCarResponse();
                }

                return new GetCarResponse
                {
                   Car = ConvertFromEntity(carEntity) 
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCar failed.", SystemErrorCode.SystemError);
            }
        }

        private GetCarsResponse ConvertEntities(List<CarEntity> allentities)
        {
            return new GetCarsResponse
            {
                CarList = allentities.Select(x => ConvertFromEntity(x)).ToList()
            };
        }

        private CarDTO ConvertFromEntity(CarEntity x)
        {
            return  new CarDTO
            {
                 CarId = x.CarId,
                 CarImageId = x.CarImageId,
                 CarModelId = x.CarModelId,
                 CreateDate = x.CreateDate,
                 CreatedBy = x.CreatedBy,
                 Localization = x.Localization,
                 Mileage = x.Mileage,
                 PricePerHour = x.PricePerHour,
                 ProductionDate = x.ProductionDate,
                 UpdateDate = x.UpdateDate,
                 UpdatedBy = x.UpdatedBy,
                 UserRating = x.UserRating                
            };
        }

        private void ValidateRequest(CarIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CarId == 0)
            {
                throw new SystemBaseException("CarId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(CarDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.Localization))
            {
                throw new SystemBaseException("Localization is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.Mileage < 0)
            {
                throw new SystemBaseException("Mileage is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.PricePerHour < 0.0)
            {
                throw new SystemBaseException("PricePerHour is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.CarModelId == 0)
            {
                throw new SystemBaseException("CarModelId is not valid.", SystemErrorCode.ValidationError);
            }

        }
    }
}
