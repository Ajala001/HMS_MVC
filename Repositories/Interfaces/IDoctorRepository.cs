using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> AddAsync(Doctor doctor);
        Doctor Update (Doctor doctor);
        void Delete(Doctor doctor);
        Task<ICollection<Doctor>> GetAllAsync();
        Task<Doctor> GetAsync(Expression<Func<Doctor, bool>> pred);
        Task<ICollection<Doctor>> GetSelectedAsync(Expression<Func<Doctor, bool>> pred);
    }
}
