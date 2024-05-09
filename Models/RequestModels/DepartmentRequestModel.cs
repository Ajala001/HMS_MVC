using HMSMVC.Entity;

namespace HMSMVC.Models.RequestModels
{
    public class DepartmentRequestModel
    {
        public required string DepartmentName { get; set; }
        public required string DepartmentDescription { get; set; }
        public required DateTime OpeningHours { get; set; }
        public required DateTime ClosingHours { get; set; }
    }
}
