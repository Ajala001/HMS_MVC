using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HMSDataContext _context;
        public DoctorRepository(HMSDataContext context)
        {
            _context = context;
        }
        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            return doctor;
        }

        public void Delete(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
        }

        public async Task<ICollection<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                   .Include(d => d.Department)
                   .Include(d => d.User)
                   .ToListAsync();
        }

        public async Task<Doctor> GetAsync(Expression<Func<Doctor, bool>> pred)
        {
            var doctor = await _context.Doctors
                       .Include(d => d.Department)
                       .Include(d => d.User)
                       .FirstOrDefaultAsync(pred);
            return doctor;
        }

        public async Task<ICollection<Doctor>> GetSelectedAsync(Expression<Func<Doctor, bool>> pred)
        {
            return await _context.Doctors
                        .Include(d => d.Department)
                        .Include(d => d.User)
                        .Where(pred)
                        .ToListAsync();
        }

        public Doctor Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            return doctor;
        }
    }
}
