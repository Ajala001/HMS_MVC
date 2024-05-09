using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HMSDataContext _context;
        public DepartmentRepository(HMSDataContext context)
        {
            _context = context;
        }
        public async Task<Department> AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            return department;
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);    
        }

        public async Task<ICollection<Department>> GetAllAsync()
        {
            return await _context.Departments
                         .ToListAsync();
        }

        public async Task<Department> GetAsync(Expression<Func<Department, bool>> pred)
        {
            var department = await _context.Departments
                            .FirstOrDefaultAsync(pred);
            return department;
        }

        public async Task<bool> IsExistAsync(string departmentName)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentName.ToLower() == departmentName.ToLower());
        }

        public Department Update(Department department)
        {
            _context.Departments.Update(department);
            return department;
        }
    }
}
