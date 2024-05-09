using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.RequestModels
{
    public class NotificationRequestModel
    {
        public string RecipientEmail { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
