using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<BaseResponse<ICollection<MedicalRecordResponseModel>>> GetMedicalRecordAsync(Guid patientId);
        Task<BaseResponse<MedicalRecordResponseModel>> CreateAsync(MedicalRecordRequestModel medicalRecordRequestModel);
        Task<BaseResponse<ICollection<MedicalRecordResponseModel>>> GetAllAsync();
        Task<BaseResponse<MedicalRecordResponseModel>> DeleteAsync(Guid medicalRecordId);
    }
}
