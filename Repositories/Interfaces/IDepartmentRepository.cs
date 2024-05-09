using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> AddAsync(Department department);
        Department Update(Department department);
        void Delete(Department department);
        Task<ICollection<Department>> GetAllAsync();
        Task<Department> GetAsync(Expression<Func<Department, bool>> pred);
        Task<bool> IsExistAsync (string departmentName);
    }
}
