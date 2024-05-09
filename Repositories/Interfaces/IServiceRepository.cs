using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service> AddAsync(Service service);
        Service Update(Service service);
        void Delete(Service service);
        Task<ICollection<Service>> GetAllAsync();
        Task<Service> GetAsync(Expression<Func<Service, bool>> pred);
    }
}
