using HMSMVC.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entity
{
    public class Doctor : Auditables
    {
        public string DoctorCode { get; set; } = default!;
        [ForeignKey ("DepartmentId")]
        public Guid DepartmentId { get; set; } = default!;
        public Department Department { get; set; } = default!;
        [ForeignKey("UserId")]
        public Guid UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public string YearOfExperience { get; set; } = default!;
        public string FieldOfSpecialization { get; set; } = default!;
		public string? SignatureUrl { get; set; }
		public ICollection<Appointment> Appointment { get; set; } = new List<Appointment>();
    }
}