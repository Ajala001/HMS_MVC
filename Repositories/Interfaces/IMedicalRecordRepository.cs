using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord> AddAsync(MedicalRecord medicalRecord);
        MedicalRecord Update(MedicalRecord medicalRecord);
        void Delete(MedicalRecord medicalRecord);
        Task<ICollection<MedicalRecord>> GetAllAsync();
        Task<MedicalRecord> GetAsync(Expression<Func<MedicalRecord, bool>> pred);
        Task<ICollection<MedicalRecord>> GetSelectedAsync(Expression<Func<MedicalRecord, bool>> pred);
    }
}
