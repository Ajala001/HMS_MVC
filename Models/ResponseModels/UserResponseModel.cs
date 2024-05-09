using HMSMVC.Entity;

namespace HMSMVC.Models.ResponseModels
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public required ICollection<RoleResponseModel> Roles { get; set; } = new List<RoleResponseModel>();
    }
}
