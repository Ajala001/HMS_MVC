using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Models.ResponseModels;
using HMSMVC.Repositories.Interfaces;
using HMSMVC.Services.Interfaces;
using Org.BouncyCastle.Cms;
using System.Reflection;
using System.Security.Claims;

namespace HMSMVC.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository; 
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IHttpContextAccessor contextAccessor)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }
        public async Task<BaseResponse<ICollection<NotificationResponseModel>>> GetReceiverNotificationsAsync(string recipientEmail)
        {
            var notifications = await _notificationRepository.GetSelectedAsync(notification => notification.RecipientEmail == recipientEmail);
            if(notifications.Count == 0)
            {
                return new BaseResponse<ICollection<NotificationResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "User has no Notification",
                    Data = null
                };
            }

            var notificationsView = new List<NotificationResponseModel>();
            foreach (var notification in notifications)
            {
                var sender = await _userRepository.GetAsync(u => u.Email == notification.SenderEmail);
                var recipient = await _userRepository.GetAsync(u => u.Email == notification.RecipientEmail);

                var notificationView = new NotificationResponseModel
                {
                    Id = notification.Id,
                    SenderFullName = $"{sender.FirstName} {sender.LastName}",
                    ReceiverFullName = $"{recipient.FirstName} {recipient.LastName}",
                    Message = notification.Message,
                    NotificationDate = notification.NotificationDate,
                    Status = notification.Status,
                };
                notificationsView.Add(notificationView);
            }

            return new BaseResponse<ICollection<NotificationResponseModel>>
            {
                IsSuccessful = true,
                Message = "Notifications Found",
                Data = notificationsView
            };
        }

        public async Task<BaseResponse<ICollection<NotificationResponseModel>>> GetSenderNotificationsAsync(string senderEmail)
        {
            var notifications = await _notificationRepository.GetSelectedAsync(notification => notification.SenderEmail == senderEmail);
            if (notifications.Count == 0)
            {
                return new BaseResponse<ICollection<NotificationResponseModel>>
                {
                    IsSuccessful = false,
                    Message = "User has no Notification",
                    Data = null
                };
            }

            var notificationsView = new List<NotificationResponseModel>();
            foreach (var notification in notifications)
            {
                var sender = await _userRepository.GetAsync(u => u.Email == notification.SenderEmail);
                var recipient = await _userRepository.GetAsync(u => u.Email == notification.RecipientEmail);

                var notificationView = new NotificationResponseModel
                {
                    Id = notification.Id,
                    SenderFullName = $"{sender.FirstName} {sender.LastName}",
                    ReceiverFullName = $"{recipient.FirstName} {recipient.LastName}",
                    Message = notification.Message,
                    NotificationDate = notification.NotificationDate,
                    Status = notification.Status,
                };
                notificationsView.Add(notificationView);
            }

            return new BaseResponse<ICollection<NotificationResponseModel>>
            {
                IsSuccessful = true,
                Message = "Notifications Found",
                Data = notificationsView
            };
        }

        public async Task<BaseResponse<NotificationResponseModel>> SendNotification(NotificationRequestModel notificationRequest)
        {
            var getRecipient = await _userRepository.GetAsync(recipient => recipient.Email == notificationRequest.RecipientEmail);
            if(getRecipient == null)
            {
                return new BaseResponse<NotificationResponseModel>
                {
                    IsSuccessful = false,
                    Message = "The User You are trying to notify does not exist",
                    Data = null
                };
            }

            var loginUser = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value;
            var sender = await _userRepository.GetAsync(u => u.Email == loginUser);
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                SenderEmail = loginUser,
                RecipientEmail = notificationRequest.RecipientEmail,
                Message = notificationRequest.Message,
                UserId = getRecipient.Id,
                User = getRecipient,
                NotificationDate = DateTime.Now,
                Status = NotificationStatus.NotViewed,
                CreatedBy = loginUser,
                CreatedOn = DateTime.Now,
            };

            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.SaveAsync();

            return new BaseResponse<NotificationResponseModel>
            {
                IsSuccessful = true,
                Message = "Notification Sent Succesfully",
                Data = new NotificationResponseModel
                {
                    Id = notification.Id,
                    SenderFullName = $"{sender.FirstName} {sender.LastName}",
                    ReceiverFullName = $"{getRecipient.FirstName} {getRecipient.LastName}",
                    Message = notification.Message,
                    NotificationDate = notification.NotificationDate,
                    Status = notification.Status,
                }
            };
            
        }
    }
}
