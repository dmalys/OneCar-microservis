using BrandService.BusinessLayer.ErrorHandling;
using BrandService.DataAccessLayer.Entities;
using BrandService.DataAccessLayer.Interfaces;
using BrandService.BusinessLayer.Brand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrandService.SyncDataService.gRPC;
using CarModelService.Proto;

namespace BrandService.BusinessLayer.Brand.Implementation
{
    public class BrandHandler : IBrandHandler
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IGrpcCarModelDataClient _grpcCarModelDataClient;

        public BrandHandler(IBrandRepository brandRepository, IGrpcCarModelDataClient grpcCarModelDataClient)
        {
            _brandRepository = brandRepository;
            _grpcCarModelDataClient = grpcCarModelDataClient;
        }

        public async Task AddBrand(AddBrandRequest request)
        {
            ValidateRequest(request);

            var brandEntity = new BrandEntity();
            brandEntity.BrandName = request.BrandName;
            brandEntity.CreateDate = DateTime.UtcNow;
            brandEntity.CreatedBy = request.CreatedBy;

            try
            {
                await _brandRepository.Insert(brandEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddBrand failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task DeleteBrand(DeleteBrandRequest request)
        {
            ValidateRequest(request);

            try
            {
                if(await _brandRepository.CheckBrandExists(request.BrandId))
                {
                    await _brandRepository.DeleteAsync(request.BrandId);

                    //notify delete
                    var notifyDeleteRequest = new NotifyDeleteBrandRequest()
                    {
                        BrandId = request.BrandId
                    };

                    _grpcCarModelDataClient.NotifyDelete(notifyDeleteRequest);
                }                
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteBrand failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetBrandResponse> GetBrand(GetBrandRequest request)
        {
            ValidateRequest(request);

            try
            {
                var carEntity = await _brandRepository.GetAsync(request.BrandId);

                if(carEntity == null)
                {
                    return new GetBrandResponse();
                }

                return new GetBrandResponse
                {
                    Brand = ConvertFromEntity(carEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetBrand failed.", SystemErrorCode.SystemError);
            }            
        }

        public async Task<GetBrandsResponse> GetBrands()
        {
            try
            {
                var allEntities = await _brandRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetBrandsResponse { BrandList = new List<BrandDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetBrands failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateBrand(UpdateBrandRequest request)
        {
            ValidateRequest(request);
            if (request.BrandId == 0)
            {
                throw new SystemBaseException("BrandId is not valid.", SystemErrorCode.ValidationError);
            }
            var brandEntity = new BrandEntity();
            brandEntity.BrandName = request.BrandName;
            brandEntity.UpdateDate = DateTime.UtcNow;
            brandEntity.UpdatedBy = request.UpdatedBy;
            brandEntity.BrandId = request.BrandId;
            try
            {
                if (await _brandRepository.CheckBrandExists(request.BrandId))
                {
                    await _brandRepository.Update(brandEntity);
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
                throw new SystemBaseException("UpdateBrand failed.", SystemErrorCode.SystemError);
            }
        }

        private GetBrandsResponse ConvertEntitiesToResponse(List<BrandEntity> allentities)
        {
            var entities = allentities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetBrandsResponse
            {
                BrandList = entities
            };
        }

        private BrandDTO ConvertFromEntity(BrandEntity x)
        {
            return new BrandDTO
            {
               BrandId = x.BrandId,
               BrandName = x.BrandName,
               CreateDate = x.CreateDate,
               CreatedBy = x.CreatedBy,
               UpdateDate = x.UpdateDate,
               UpdatedBy = x.UpdatedBy
            };
        }

        private void ValidateRequest(BrandIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.BrandId == 0)
            {
                throw new SystemBaseException("BrandId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(BrandDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.BrandName))
            {
                throw new SystemBaseException("BrandName is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
