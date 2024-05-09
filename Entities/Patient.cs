using HMSMVC.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entity
{
    public class Patient : Auditables
    {
        public string PatientCode { get; set; } = default!;
        [ForeignKey("UserId")]
        public Guid UserId { get; set; } = default!;
        public User User { get; set; } = default!; 
        public Wallet? Wallet { get; set; }
        public bool HasScheduleAppointment { get; set; }
        public ICollection<MedicalRecord> MedicalHistory { get; set; } = new List<MedicalRecord>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<PatientServices> PatientServices { get; set; } = new List<PatientServices>();
    }
}