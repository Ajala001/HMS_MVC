using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.RequestModels
{
    public class AppointmentRequestModel
    {
        public Guid PatientId { get; set; } = default!;
        public Guid ServiceId { get; set; } = default!;
        public required string Complain { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
