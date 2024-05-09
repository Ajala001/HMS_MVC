using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceService(IServiceRepository serviceRepository, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }
        public async Task<BaseResponse<ServiceResponseModel>> CreateAsync(ServiceRequestModel serviceModel)
        {
            var service = await _serviceRepository.GetAsync(s => s.ServiceName == serviceModel.Name);
            if(service != null)
            {
                return new BaseResponse<ServiceResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Service Already Exist",
                    Data = null
                };
            }

            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var newService = new Service
            {
                Id = Guid.NewGuid(),
                ServiceName = serviceModel.Name,
                ServiceDescription = serviceModel.Description,
                Amount = serviceModel.Amount,
                CreatedBy = loginUser,
                CreatedOn = DateTime.UtcNow,
            };
            await _serviceRepository.AddAsync(newService);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<ServiceResponseModel>
            {
                IsSuccessful = true,
                Message = "Service Created Successfully",
                Data = new ServiceResponseModel
                {
                    Id = newService.Id,
                    Name = newService.ServiceName,
                    Description = newService.ServiceDescription,
                    Amount = newService.Amount,
                }
            };
        }

        public async Task<BaseResponse<ServiceResponseModel>> DeleteAsync(Guid serviceId)
        {
            var service = await _serviceRepository.GetAsync(s => s.Id == serviceId);
            if(service == null)
            {
                return new BaseResponse<ServiceResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Service Not Found",
                    Data = null
                };
            }

            _serviceRepository.Delete(service);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<ServiceResponseModel>
            {
                IsSuccessful = true,
                Message = "Service Deleted Successfully",
                Data = null
            };
        }

        public async Task<BaseResponse<ICollection<ServiceResponseModel>>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            if(services.Count == 0)
            {
                return new BaseResponse<ICollection<ServiceResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "Services Not Found",
                    Data = null
                };
            }

            var serviceView = services.Select(service => new ServiceResponseModel
            {
                Id = service.Id,
                Name = service.ServiceName,
                Description = service.ServiceDescription,
                Amount = service.Amount,
            }).ToList();

            return new BaseResponse<ICollection<ServiceResponseModel>>
            {
                IsSuccessful = true,
                Message = "Services Found",
                Data = serviceView
            };
        }

        public async Task<BaseResponse<ServiceResponseModel>> GetAsync(Guid serviceId)
        {
            var service = await _serviceRepository.GetAsync(s => s.Id == serviceId);
            if (service == null)
            {
                return new BaseResponse<ServiceResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Service Not Found",
                    Data = null
                };
            }

            return new BaseResponse<ServiceResponseModel>
            {
                IsSuccessful = true,
                Message = "Service Found",
                Data = new ServiceResponseModel
                { 
                    Id = serviceId,
                    Name = service.ServiceName,
                    Description = service.ServiceDescription,
                    Amount = service.Amount,
                }
            };
        }

        public async Task<BaseResponse<ServiceResponseModel>> UpdateAsync(Guid serviceId, ServiceUpdateModel serviceUpdateModel)
        {
            var service = await _serviceRepository.GetAsync(s => s.Id == serviceId);
            if (service == null)
            {
                return new BaseResponse<ServiceResponseModel>
                {
                    IsSuccessful = false,
                    Message = "Service Not Found",
                    Data = null
                };
            }

			var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
			service.ServiceName = serviceUpdateModel.Name ?? service.ServiceName;
            service.ServiceDescription = serviceUpdateModel.Description ?? service.ServiceDescription;
            service.Amount = serviceUpdateModel.Amount ?? service.Amount;
            service.ModifiedOn = DateTime.Now;
            service.ModifiedBy = loginUser;
            _serviceRepository.Update(service);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<ServiceResponseModel>
            {
                IsSuccessful = true,
                Message = "Updated Successfully",
                Data = new ServiceResponseModel
                {
                    Id = service.Id,
                    Name = service.ServiceName,
                    Description = service.ServiceDescription,
                    Amount = service.Amount
                }
            };
        }
    }
}
