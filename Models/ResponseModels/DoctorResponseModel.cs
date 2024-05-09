using System.ComponentModel.DataAnnotations;

namespace HMSMVC.Models.ResponseModels
{
    public class DoctorResponseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string DepartmentName { get; set; } = default!;
        public Gender Gender { get; set; }
    }
}
