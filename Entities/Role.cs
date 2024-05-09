using Microsoft.AspNetCore.Identity;

namespace HMSMVC.Entity
{
    public class Role : IdentityRole<Guid>
	{
		public Guid Id { get; set; }
        public string? Description { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; } = default!;
		public DateTime? ModifiedOn { get; set; }
		public string? ModifiedBy { get; set; }
		public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
