using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.ResponseModels
{
    public class NotificationResponseModel
    {
        public Guid Id { get; set; }
        public string? SenderFullName { get; set; }
        public string? ReceiverFullName { get; set; }
        public string Message { get; set; } = default!;
        public NotificationStatus Status { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
