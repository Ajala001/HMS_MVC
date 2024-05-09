using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleResponseModel>> CreateAsync(RoleRequestModel roleRequestModel);
        Task<BaseResponse<RoleResponseModel>> GetAsync(Guid roleId);
        Task<BaseResponse<RoleResponseModel>> UpdateAsync(RoleUpdateModel roleUpdateModel);
        Task<BaseResponse<RoleResponseModel>> DeleteAsync (Guid roleId);
        Task<BaseResponse<ICollection<RoleResponseModel>>> GetAllAsync();
    }
}
