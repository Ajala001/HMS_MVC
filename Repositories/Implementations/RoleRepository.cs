using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HMSDataContext _context;
        public RoleRepository(HMSDataContext context)
        {
            _context = context;
        }
        public async Task<Role> AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            return role;
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            return await _context.Roles
                         .ToListAsync();
        }

        public async Task<Role> GetAsync(Expression<Func<Role, bool>> pred)
        {
            var role = await _context.Roles
                       .Include(r => r.UserRoles)
                       .ThenInclude(r => r.User)
                       .FirstOrDefaultAsync(pred);
            return role;
        }

        public Role Update(Role role)
        {
            _context.Roles.Update(role);
            return role;
        }
    }
}
