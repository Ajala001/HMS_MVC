using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSMVC.Entity
{
    public class UserRoles
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey ("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
