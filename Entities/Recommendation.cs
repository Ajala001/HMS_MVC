using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations;

namespace HMSMVC.Entities
{
    public class Recommendation 
    {
        [Key]
        public Guid Id { get; set; }
        public required string DoctorRecommendation1 { get; set; }
        public required string DoctorRecommendation2 { get; set; } 
        public required string DoctorRecommendation3 { get; set; }
        public string? DoctorRecommendation4 { get; set; }
        public string? DoctorRecommendation5 { get; set; }
        public Guid MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; } = default!;
    }
}
