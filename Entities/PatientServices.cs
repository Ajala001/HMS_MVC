using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entities
{
    public class PatientServices
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("PatientId")]
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }
        [ForeignKey("ServiceId")]
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}
