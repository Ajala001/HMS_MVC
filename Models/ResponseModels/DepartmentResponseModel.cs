using HMSMVC.Entity;

namespace HMSMVC.Models.ResponseModels
{
    public class DepartmentResponseModel
    {
        public Guid Id { get; set; }
        public required string DepartmentName { get; set; }
        public DateTime OpeningHours { get; set; }
        public DateTime ClosingHours { get; set; }
    }
}
