namespace HMSMVC.Models.ResponseModels
{
    public class PatientResponseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string PatientCode { get; set; } = default!;
        public Gender Gender { get; set; }
    }
}
