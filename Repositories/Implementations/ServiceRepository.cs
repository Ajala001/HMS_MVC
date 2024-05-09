using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class ServiceRepository(HMSDataContext _context) : IServiceRepository
    {
        public async Task<Service> AddAsync(Service service)
        {
            await _context.Services.AddAsync (service);
            return service;
        }

        public void Delete(Service service)
        {
            _context.Services.Remove(service);
        }

        public async Task<ICollection<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetAsync(Expression<Func<Service, bool>> pred)
        {
            var response = await _context.Services.FirstOrDefaultAsync(pred);
            return response;
        }

        public Service Update(Service service)
        {
            _context.Services.Update(service);
            return service;
        }
    }
}
