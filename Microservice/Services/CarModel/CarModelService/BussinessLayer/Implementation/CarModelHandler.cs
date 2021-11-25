using CarModelService.AsyncDataService;
using CarModelService.BusinessLayer.CarImage.Interfaces;
using CarModelService.BusinessLayer.CarModel.Models;
using CarModelService.BusinessLayer.ErrorHandling;
using CarModelService.DataAccessLayer.Entities;
using CarModelService.DataAccessLayer.Interfaces;
using CarModelService.SyncDataService.gRPC;
using CarModelService.SyncDataServices;
using CarService.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.BusinessLayer.CarImage.Implementation
{
    public class CarModelHandler : ICarModelHandler
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly ICarModelsRepository _carModelRepository;
        private readonly IHttpBrandDataClient _brandDataClient;
        private readonly IGrpcBrandDataClient _grpcBrandDataClient;
        private readonly IGrpcCarDataClient _grpcCarDataClient;

        public CarModelHandler(
            ICarModelsRepository carModelRepository,
            IHttpBrandDataClient brandDataClient, 
            IMessageBusClient messageBusClient,
            IGrpcBrandDataClient grpcBrandDataClient,
            IGrpcCarDataClient grpcCarDataClient)
        {
            _messageBusClient = messageBusClient;
            _carModelRepository = carModelRepository;
            _brandDataClient = brandDataClient;
            _grpcBrandDataClient = grpcBrandDataClient;
            _grpcCarDataClient = grpcCarDataClient;
        }

        public async Task AddCarModel(AddCarModelRequest request)
        {
            ValidateRequest(request);

            var carModelEntity = new CarModelEntity();
            carModelEntity.CarModelName = request.CarModelName;
            carModelEntity.CreateDate = DateTime.UtcNow;
            carModelEntity.CreatedBy = request.CreatedBy;
            carModelEntity.BrandId = request.BrandId;

            try
            {
                //change try try //TODO:

                //try
                //{
                //    //Sync messaging TODO:
                //    var brandClientRequest = new SendAddCarModelRequest
                //    {
                //        BrandId = request.BrandId
                //    };
                //    await _brandDataClient.SendCheckForAddCarModel(brandClientRequest);
                //}
                //catch (Exception ex)
                //{
                //    throw new SystemBaseException("Sync message failed.", SystemErrorCode.SystemError, ex);
                //}
               
                //Async messagin TODO:
                //var checkBrandExistsDTO = new BrandPublishDTO
                //{
                //    BrandId = request.BrandId,
                //    Event = "CheckBrand"
                //};

                //TODO: change to return bool
                //rabbitmq pubsub not usable
                //await _messageBusClient.PublishCheckBrandExists(checkBrandExistsDTO);

                var checkBrandRequest = new BrandService.Proto.CheckBrandRequest
                {
                    BrandId = request.BrandId
                };
                var isBrandExisting = _grpcBrandDataClient.CheckBrandExist(checkBrandRequest);

                if (isBrandExisting)
                {
                    await _carModelRepository.Insert(carModelEntity);
                }
                else
                {
                    throw new SystemBaseException("Brand is not existing.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (SystemBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SystemBaseException("AddCarModel failed.", SystemErrorCode.SystemError, ex);
            }
        }

        public async Task DeleteCarModel(DeleteCarModelRequest request)
        {
            ValidateRequest(request);

            try
            {
                if(await _carModelRepository.CheckCarModelExists(request.CarModelId))
                {
                    await _carModelRepository.DeleteAsync(request.CarModelId);

                    //notify delete
                    var carModelRequest = new NotifyDeleteCarModelRequest();
                    carModelRequest.CarModelIds.Add(request.CarModelId);

                    _grpcCarDataClient.NotifyDelete(carModelRequest);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteCarModel failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCarModelResponse> GetCarModel(GetCarModelRequest request)
        {
            ValidateRequest(request);

            try
            {
                var carEntity = await _carModelRepository.GetAsync(request.CarModelId);

                if(carEntity == null)
                {
                    return new GetCarModelResponse();
                }

                return new GetCarModelResponse
                {
                    CarModel = ConvertFromEntity(carEntity)
                };
            }
            catch (Exception ex)
            {
                throw new SystemBaseException("GetCarModel failed.", SystemErrorCode.SystemError, ex);
            }            
        }

        public async Task<GetCarModelsResponse> GetCarModels()
        {
            try
            {
                var allEntities = await _carModelRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetCarModelsResponse { CarModelList = new List<CarModelDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception ex)
            {
                throw new SystemBaseException("GetCarModels failed.", SystemErrorCode.SystemError, ex);
            }
        }

        public async Task UpdateCarModel(UpdateCarModelRequest request)
        {
            ValidateRequest(request);
            if(request.CarModelId == 0)
            {
                throw new SystemBaseException("CarModelId is not valid.", SystemErrorCode.ValidationError);
            }

            var carModelEntity = new CarModelEntity();
            carModelEntity.CarModelName = request.CarModelName;
            carModelEntity.UpdateDate = DateTime.UtcNow;
            carModelEntity.UpdatedBy = request.UpdatedBy;
            carModelEntity.BrandId = request.BrandId;
            carModelEntity.CarModelId = request.CarModelId;

            try
            {
                var checkBrandRequest = new BrandService.Proto.CheckBrandRequest
                {
                    BrandId = request.BrandId
                };
                var isBrandExisting = _grpcBrandDataClient.CheckBrandExist(checkBrandRequest);

                if (isBrandExisting)
                {
                    if (await _carModelRepository.CheckCarModelExists(request.CarModelId))
                    {
                        await _carModelRepository.Update(carModelEntity);
                    }
                    else
                    {
                        throw new SystemBaseException("Entity not found", SystemErrorCode.EntityNotFound);
                    }
                }
                else
                {
                    throw new SystemBaseException("Brand is not existing.", SystemErrorCode.EntityNotFound);
                }               
            }
            catch (SystemBaseException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateCarModel failed.", SystemErrorCode.SystemError);
            }
        }

        private GetCarModelsResponse ConvertEntitiesToResponse(List<CarModelEntity> allentities)
        {
            var entities = allentities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetCarModelsResponse
            {
                CarModelList = entities
            };
        }

        private CarModelDTO ConvertFromEntity(CarModelEntity x)
        {
            return new CarModelDTO
            {
                CarModelId = x.CarModelId,
                CarModelName = x.CarModelName,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.UpdatedBy,
                BrandId = x.BrandId
            };
        }

        private void ValidateRequest(CarModelIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CarModelId == 0)
            {
                throw new SystemBaseException("CarModelId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(CarModelDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.BrandId == 0)
            {
                throw new SystemBaseException("BrandId is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.CarModelName))
            {
                throw new SystemBaseException("CarModelName is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
