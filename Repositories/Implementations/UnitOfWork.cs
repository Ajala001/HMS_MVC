using HMSMVC.Data;
using HMSMVC.Repositories.Interfaces;

namespace HMSMVC.Repositories.Implementations
{
    public class UnitOfWork(HMSDataContext _context) : IUnitOfWork
    {
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
