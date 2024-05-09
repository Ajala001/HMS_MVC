using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.ResponseModels
{
    public class AppointmentResponseModel
    {
        public Guid Id { get; set; }
        public string PatientFullName { get; set; } = default!;
        public string ServiceName { get; set; } = default!;
        public AppointmentStatus Status { get; set; }
        public DateTime AppointmentDateAndTime { get; set; }
    }
}
