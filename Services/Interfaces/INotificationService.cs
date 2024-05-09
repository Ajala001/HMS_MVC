using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;

namespace HMSMVC.Services.Interfaces
{
    public interface INotificationService
    {
        Task<BaseResponse<ICollection<NotificationResponseModel>>> GetSenderNotificationsAsync(string senderEmail);
        Task<BaseResponse<ICollection<NotificationResponseModel>>> GetReceiverNotificationsAsync(string recipientEmail);
        Task<BaseResponse<NotificationResponseModel>> SendNotification(NotificationRequestModel notificationRequest);
    }
}
