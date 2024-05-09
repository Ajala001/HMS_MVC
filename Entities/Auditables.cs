namespace HMSMVC.Entity
{
    public class Auditables
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set;}
    }
}
