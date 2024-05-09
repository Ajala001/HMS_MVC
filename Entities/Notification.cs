using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entity
{
    public class Notification : Auditables
    {
        public string RecipientEmail { get; set; } = default!;
        public string SenderEmail { get; set; } = default!;
        [ForeignKey ("UserId")]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Message { get; set; } = default!;
        public NotificationStatus Status { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}