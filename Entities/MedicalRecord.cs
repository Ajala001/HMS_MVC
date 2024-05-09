using HMSMVC.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entity
{
    public class MedicalRecord : Auditables
    {
        [ForeignKey("AppointmentId")]
        public Guid AppointmentId { get; set; } = default!;
        public Appointment Appointment { get; set; } = default!;
        public DateTime DateRecorded { get; set; }
        public string? DoctorReport { get; set; }
        public string MedicalRecDept { get; set; } = default!;
        public Recommendation DoctorRecommendations { get; set; } = default!;
    }
}