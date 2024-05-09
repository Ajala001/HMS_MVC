using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class MedicalRecordRepository(HMSDataContext _context) : IMedicalRecordRepository
    {
		public async Task<MedicalRecord> AddAsync(MedicalRecord medicalRecord)
		{
			await _context.MedicalRecords.AddAsync(medicalRecord);
			return medicalRecord;
		}

		public void Delete(MedicalRecord medicalRecord)
		{
			_context.MedicalRecords.Remove(medicalRecord);
		}

		public async Task<ICollection<MedicalRecord>> GetAllAsync()
		{
			return await _context.MedicalRecords
						 .Include(a => a.Appointment)
						 .ToListAsync();
		}

		public async Task<MedicalRecord> GetAsync(Expression<Func<MedicalRecord, bool>> pred)
		{
			var response = await _context.MedicalRecords
						   .Include(a => a.Appointment)
						   .ThenInclude(a => a.Patient)
						   .ThenInclude(a => a.User)
						   .FirstOrDefaultAsync(pred);
			return response;
		}

		public async Task<ICollection<MedicalRecord>> GetSelectedAsync(Expression<Func<MedicalRecord, bool>> pred)
		{
			var response = await _context.MedicalRecords.Where(pred).ToListAsync();
			return response;
		}

		public MedicalRecord Update(MedicalRecord medicalRecord)
		{
			_context.MedicalRecords.Update(medicalRecord);
			return medicalRecord;
		}
	}
}
