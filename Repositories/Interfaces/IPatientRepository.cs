using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient patient);
        Patient Update(Patient patient);
        void Delete(Patient patient);
        Task<ICollection<Patient>> GetAllAsync();
        Task<Patient> GetAsync(Expression<Func<Patient, bool>> pred);
    }
}
