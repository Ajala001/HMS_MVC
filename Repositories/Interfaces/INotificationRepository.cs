using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddAsync(Notification notification);
        Notification Update(Notification notification);
        void Delete(Notification notification);
        Task<ICollection<Notification>> GetAllAsync();
        Task<ICollection<Notification>> GetSelectedAsync(Expression<Func<Notification, bool>> pred);
        Task<Notification> GetAsync(Expression<Func<Notification, bool>> pred);
    }
}
