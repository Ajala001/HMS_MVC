using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entity
{
    public class Appointment : Auditables
    {
        [ForeignKey ("PatientId")]
        public Guid PatientId { get; set; } = default!;
        public Patient Patient { get; set; } = default!;
        [ForeignKey ("DoctorId")]
        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        [ForeignKey("ServiceId")]
        public Guid ServiceId { get; set; } = default!;
        public Service Service { get; set; } = default!;
        public AppointmentStatus Status { get; set; }
        public string? Complain { get; set; }
        public DateTime AppointmentDateAndTime { get; set; }
        [ForeignKey("DepartmentId")]
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}