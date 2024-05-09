using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;
using System.Linq.Expressions;


namespace HMSMVC.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<BaseResponse<DoctorResponseModel>> CreateAsync(DoctorRequestModel doctorRequestModel);
        Task<BaseResponse<DoctorResponseModel>> UpdateAsync(Guid id, DoctorUpdateModel updateModel);
        Task<BaseResponse<DoctorResponseModel>> DeleteAsync(Guid id);
        Task<BaseResponse<DoctorResponseModel>> GetAsync(Guid id);
        Task<BaseResponse<ICollection<DoctorResponseModel>>> GetAllAsync();
        Task<BaseResponse<ICollection<DoctorResponseModel>>> GetSelectedAsync(string departmentName);
    }
}
