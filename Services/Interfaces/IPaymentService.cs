using HMSMVC.Models.ResponseModels;

namespace HMSMVC.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponse<PaymentResponseModel>> CreateAsync(Guid ServiceId);
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllAsync();
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetPatientPaymentsAsync(Guid patientId);
        Task<BaseResponse<ICollection<PaymentResponseModel>>> GetServicePaymentsAsync(Guid serviceId);


    }
}
