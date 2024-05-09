namespace HMSMVC.Entity
{
    public class Department : Auditables
    {
        public required string DepartmentName { get; set; }
        public string? DepartmentDescription { get; set; }
        public string? HeadOfDepartment { get; set; }
        public required DateTime OpeningHours { get; set; }
        public required DateTime ClosingHours { get; set; }
        public ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();
    }
}