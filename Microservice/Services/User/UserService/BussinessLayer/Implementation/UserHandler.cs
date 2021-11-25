using UserService.BusinessLayer.ErrorHandling;
using UserService.BusinessLayer.User.Interfaces;
using UserService.BusinessLayer.User.Models;
using UserService.DataAccessLayer.Entities;
using UserService.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.SyncDataService.gRPC;
using CouponService.Proto;
using CarService.Proto;
using AccountService.Proto;

namespace UserService.BusinessLayer.User.Implementation
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository;

        private readonly IGrpcCouponDataClient _grpcCouponDataClient;
        private readonly IGrpcCarDataClient _grpcCarDataClient;
        private readonly IGrpcAccountDataClient _grpcAccountDataClient;

        public UserHandler(IUserRepository userRepository,
            IGrpcCouponDataClient grpcCouponDataClient,
            IGrpcCarDataClient grpcCarDataClient,
            IGrpcAccountDataClient grpcAccountDataClient
            )
        {
            _userRepository = userRepository;
            _grpcCouponDataClient = grpcCouponDataClient;
            _grpcCarDataClient = grpcCarDataClient;
            _grpcAccountDataClient = grpcAccountDataClient;
        }

        public async Task AddUser(AddUserRequest request)
        {
            ValidateRequest(request);
            if (request.AccountId == 0)
            {
                throw new SystemBaseException("AccountId is not valid.", SystemErrorCode.ValidationError);
            }

            var checkAccountExistRequest = new CheckAccountRequest
            {
                AccountId = request.AccountId
            };
            if (!_grpcAccountDataClient.CheckAccountExist(checkAccountExistRequest))
            {
                throw new SystemBaseException("Account was not found .", SystemErrorCode.EntityNotFound);
            }          

            var userEntity = new UserEntity();
            userEntity.CreateDate = DateTime.UtcNow;            
            userEntity.AccountId = request.AccountId;
            userEntity.AvailableCredit = request.AvailableCredit;
            userEntity.City = request.City;
            userEntity.Country = request.Country;
            userEntity.DrivingLicenseId = request.DrivingLicenseId;
            userEntity.Email = request.Email;
            userEntity.FirstName = request.FirstName;
            userEntity.Gender = request.Gender;
            userEntity.LastName = request.LastName;
            userEntity.Phone = request.Phone;
            userEntity.State = request.State;
            userEntity.Street = request.Street;
            userEntity.ZipCode = request.ZipCode;

            try
            {
                await _userRepository.Insert(userEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("InsertUser failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task DeleteUser(DeleteUserRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _userRepository.CheckUserExists(request.UserId))
                {
                    await _userRepository.DeleteAsync(request.UserId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteUser failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetUserResponse> GetUser(GetUserRequest request)
        {
            ValidateRequest(request);

            try
            {
                var userEntity = await _userRepository.GetAsync( request.UserId);

                if (userEntity == null)
                {
                    return new GetUserResponse();
                }

                return new GetUserResponse
                {
                    User = ConvertFromEntity(userEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetUser failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetUsersResponse> GetUsers()
        {
            try
            {
                var allentities = await _userRepository.GetAll();

                if(allentities.Count == 0)
                {
                    return new GetUsersResponse 
                    {
                        UserList = new List<UserDTO>()
                    };
                }

                return ConvertEntitiesToResponse(allentities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetUsers failed.", SystemErrorCode.SystemError);
            }           
        }

        public async Task RentCar(RentCarRequest request)
        {
            ValidateRequest(request);

            try
            {
                var userEntity = await _userRepository.GetAsync(request.UserId);

                if (userEntity == null)
                {
                    throw new SystemBaseException("User not found.", SystemErrorCode.EntityNotFound);
                }

                var grpcGetCarDataRequest = new GetCarDataRequest
                {
                    CarId = request.CarId
                };
                var carData = _grpcCarDataClient.GetCarData(grpcGetCarDataRequest);

                if (carData == null)
                {
                    throw new SystemBaseException("Car not found.", SystemErrorCode.EntityNotFound);
                }

                var creditRemovalFormula = userEntity.AvailableCredit - carData.PricePerHour * request.AmountOfHours;
                if (creditRemovalFormula < 0.0)
                {
                    throw new SystemBaseException("Credits missing.", SystemErrorCode.CreditsMissing);
                }

                userEntity.CarId = request.CarId;
                userEntity.AvailableCredit = creditRemovalFormula;

                await _userRepository.Update(userEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("RentCar failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateUser(UpdateUserRequest request)
        {
            ValidateRequest(request);
            if(request.UserId == 0)
            {
                throw new SystemBaseException("UserId is not valid.", SystemErrorCode.ValidationError);
            }
            if (request.AccountId == 0)
            {
                throw new SystemBaseException("AccountId is not valid.", SystemErrorCode.ValidationError);
            }

            var checkAccountExistRequest = new CheckAccountRequest
            {
                AccountId = request.AccountId
            };
            if (!_grpcAccountDataClient.CheckAccountExist(checkAccountExistRequest))
            {
                throw new SystemBaseException("Account was not found .", SystemErrorCode.EntityNotFound);
            }

            var userEntity = new UserEntity();
            userEntity.UpdateDate = DateTime.UtcNow;
            userEntity.AccountId = request.AccountId;
            userEntity.AvailableCredit = request.AvailableCredit;
            userEntity.Email = request.Email;
            userEntity.DrivingLicenseId = request.DrivingLicenseId;
            userEntity.FirstName = request.FirstName;
            userEntity.LastName = request.LastName;
            userEntity.Gender = request.Gender;
            userEntity.UserId = request.UserId;

            if (!string.IsNullOrWhiteSpace(request.City))
            {
                userEntity.City = request.City.Trim();
            }
            if (!string.IsNullOrWhiteSpace(request.Country))
            {
                userEntity.Country = request.Country.Trim();
            }
            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                userEntity.Phone = request.Phone.Trim();
            }
            if (!string.IsNullOrWhiteSpace(request.State))
            {
                userEntity.State = request.State.Trim();
            }
            if (!string.IsNullOrWhiteSpace(request.Street))
            {
                userEntity.Street = request.Street.Trim();
            }
            if (!string.IsNullOrWhiteSpace(request.ZipCode))
            {
                userEntity.ZipCode = request.ZipCode.Trim();
            }

            try
            {
                if(await _userRepository.CheckUserExists(request.UserId))
                {
                    await _userRepository.Update(userEntity);
                }
                else
                {
                    throw new SystemBaseException("Entity not found.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("RentCar failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task AddCouponMoneyValue(AddCouponMoneyValueRequest request)
        {
            ValidateRequest(request);

            var grpcCouponRequest = new GetCouponDataRequest
            {
                CouponCode = request.CouponCode
            };
            var coupon = _grpcCouponDataClient.GetCouponData(grpcCouponRequest);

            if (coupon == null)
                return;

            var userEntity = await _userRepository.GetAsync(request.UserId);

            if (userEntity != null && coupon.ExpirationDate > DateTime.UtcNow)
            {
                //update value
                userEntity.AvailableCredit = userEntity.AvailableCredit + coupon.MoneyValue;
                await _userRepository.Update(userEntity);

                var grpcNotifyDeleteCouponRequest = new NotifyDeleteCouponRequest
                {
                    CouponId = coupon.CouponId
                };
                _grpcCouponDataClient.NotifyDeleteCouponEntity(grpcNotifyDeleteCouponRequest);
            }
        }

        private GetUsersResponse ConvertEntitiesToResponse(List<UserEntity> allentities)
        {
            var entities = allentities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetUsersResponse
            {
                UserList = entities
            };
        }

        private UserDTO ConvertFromEntity(UserEntity x)
        {
            return new UserDTO
            {
                UserId = x.UserId,
                AccountId = x.AccountId,
                AvailableCredit = x.AvailableCredit,
                CarId = x.CarId,
                City = x.City,
                Country = x.Country,
                DrivingLicenseId = x.DrivingLicenseId,
                Email = x.Email,
                FirstName = x.FirstName,
                Gender = x.Gender,
                LastName = x.LastName,
                Phone = x.Phone,
                State = x.State,
                Street = x.Street,
                ZipCode = x.ZipCode
            };
        }

        private void ValidateRequest(AddCouponMoneyValueRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.UserId == 0)
            {
                throw new SystemBaseException("UserId is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.CouponCode))
            {
                throw new SystemBaseException("CouponCode is not valid.", SystemErrorCode.ValidationError);
            }
        }


        private void ValidateRequest(RentCarRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.UserId == 0)
            {
                throw new SystemBaseException("UserId is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.CarId == 0)
            {
                throw new SystemBaseException("CarId is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.AmountOfHours == 0)
            {
                throw new SystemBaseException("AmountOfHours is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(UserDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.DrivingLicenseId))
            {
                throw new SystemBaseException("DrivingLicenseId is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                throw new SystemBaseException("FirstName is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new SystemBaseException("LastName is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new SystemBaseException("Email is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.AvailableCredit < 0.0)
            {
                throw new SystemBaseException("AvailableCredit is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(UserIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.UserId == 0)
            {
                throw new SystemBaseException("UserId is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
