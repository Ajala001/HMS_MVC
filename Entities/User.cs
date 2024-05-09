using Microsoft.AspNetCore.Identity;

namespace HMSMVC.Entity
{
    public class User : IdentityUser<Guid>
    {
        public Guid Id {  get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public required string Password { get; set; }
        public string? Address { get; set; }
        //public required string Pin { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; } = default!;
		public DateTime? ModifiedOn { get; set; }
		public string? ModifiedBy { get; set; }
		public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    }
}