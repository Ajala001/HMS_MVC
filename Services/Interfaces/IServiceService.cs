using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IServiceService
    {
        Task<BaseResponse<ServiceResponseModel>> CreateAsync(ServiceRequestModel serviceModel);
        Task<BaseResponse<ServiceResponseModel>> UpdateAsync(Guid serviceId, ServiceUpdateModel serviceUpdateModel);
        Task<BaseResponse<ServiceResponseModel>> DeleteAsync(Guid serviceId);
        Task<BaseResponse<ServiceResponseModel>> GetAsync(Guid serviceId);
        Task<BaseResponse<ICollection<ServiceResponseModel>>> GetAllAsync();
    }
}
