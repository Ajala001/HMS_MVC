using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Models.RequestModels
{
    public class MedicalRequestModel
    {
        [ForeignKey("AppointmentId")]
        public Guid AppointmentId { get; set; } = default!;
        public Appointment Appointment { get; set; } = default!;
        public DateTime DateRecorded { get; set; }
        public string? DocReport { get; set; }
        public string MedicalRecDept { get; set; } = default!;
    }
}
