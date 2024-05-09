using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<BaseResponse<DepartmentResponseModel>> CreateAsync(DepartmentRequestModel requestModel);
        Task<BaseResponse<ICollection<DepartmentResponseModel>>> GetAllAsync();
        Task<BaseResponse<DepartmentResponseModel>> GetAsync(Guid id);
        Task<BaseResponse<DepartmentResponseModel>> DeleteAsync(Guid id);
        Task<BaseResponse<DepartmentResponseModel>> UpdateAsync(Guid id, DepartmentUpdateModel departmentUpdateModel);
    }
}
