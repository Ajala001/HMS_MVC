using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Models.UpdateModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<BaseResponse<AppointmentResponseModel>> CreateAsync(AppointmentRequestModel requestModel);
        Task<BaseResponse<ICollection<AppointmentResponseModel>>> GetAllAsync();
        Task<BaseResponse<AppointmentResponseModel>> GetAsync(Guid id);
        Task<BaseResponse<AppointmentResponseModel>> DeletAsync(Guid id);
        Task<BaseResponse<AppointmentResponseModel>> UpdateAsync(Guid id, AppointmentUpdateModel appointmentUpdateModel);
    }
}
