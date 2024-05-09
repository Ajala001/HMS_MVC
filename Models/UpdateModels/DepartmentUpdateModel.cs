using HMSMVC.Entity;

namespace HMSMVC.Models.UpdateModels
{
	public class DepartmentUpdateModel
	{
		public string? DepartmentName { get; set; }
		public string? DepartmentDescription { get; set; }
		public string? HeadOfDepartment { get; set; }
		public DateTime OpeningHours { get; set; }
		public DateTime ClosingHours { get; set; }
	}
}
