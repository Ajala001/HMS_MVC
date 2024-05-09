using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> AddAsync(Role role);
        Role Update(Role role);
        void Delete(Role role);
        Task<ICollection<Role>> GetAllAsync();
        Task<Role> GetAsync(Expression<Func<Role, bool>> pred);
    }
}
