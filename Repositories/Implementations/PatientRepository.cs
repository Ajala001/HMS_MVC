using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class PatientRepository(HMSDataContext _context) : IPatientRepository
    {
        public async Task<Patient> AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            return patient;
        }

        public void Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
        }

        public async Task<ICollection<Patient>> GetAllAsync()
        {
            return await _context.Patients
                         .Include(p => p.User)
                         .ToListAsync();
        }

        public async Task<Patient> GetAsync(Expression<Func<Patient, bool>> pred)
        {
            var response = await _context.Patients
                                .Include(p => p.User)
                                .FirstOrDefaultAsync(pred);
            return response;
        }

        public Patient Update(Patient patient)
        {
            _context.Patients.Update(patient);
            return patient;
        }
    }
}
