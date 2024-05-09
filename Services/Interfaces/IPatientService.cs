using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IPatientService
    {
        Task<BaseResponse<PatientResponseModel>> CreateAsync (PatientRequestModel patientRequestModel);
        Task<BaseResponse<PatientResponseModel>> UpdateAsync (Guid id, PatientUpdateModel patientUpdateModel);
        Task<BaseResponse<PatientResponseModel>> DeleteAsync (Guid id);
        Task<BaseResponse<PatientResponseModel>> GetAsync (Guid id);
        Task<BaseResponse<ICollection<PatientResponseModel>>> GetAllAsync();
    }
}
