using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HMSDataContext _context;
        public AppointmentRepository(HMSDataContext context)
        {
            _context = context;
        }
        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return appointment;
        }

        public void Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public async Task<ICollection<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                   .Include(a => a.Patient)
                   .Include(a => a.Doctor)
                   .Include(a => a.Department).ToListAsync();
        }

        public async Task<Appointment> GetAsync(Expression<Func<Appointment, bool>> pred)
        {
            var response = await _context.Appointments
                           .Include(a => a.Patient)
                           .Include(a => a.Doctor)
                           .Include(a => a.Department)
                           .FirstOrDefaultAsync(pred);
            return response;

        }

        public Appointment Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return appointment;
        }
    }
}
