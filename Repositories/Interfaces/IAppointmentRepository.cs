using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);
        Appointment Update(Appointment appointment);
        void Delete(Appointment appointment);
        Task<ICollection<Appointment>> GetAllAsync();
        Task<Appointment> GetAsync(Expression<Func<Appointment, bool>> pred);
    }
}
