using HMSMVC.Entity;
using System.ComponentModel.DataAnnotations;

namespace HMSMVC.Models.RequestModels
{
    public class DoctorRequestModel
    {
        public required string UserEmail { get; set; }
        public Guid DepartmentId { get; set; } = default!;
        public required string YearOfExperience { get; set; }
        public required string FieldOfSpecialization { get; set; }
    }
}
