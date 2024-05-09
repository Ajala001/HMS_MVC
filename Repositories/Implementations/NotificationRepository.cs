using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class NotificationRepository(HMSDataContext _context) : INotificationRepository
    {
        public async Task<Notification> AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            return notification;
        }

        public void Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }

        public async Task<ICollection<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                         .ToListAsync();
            // await Task.WhenAll() Find About this.
        }

        public async Task<Notification> GetAsync(Expression<Func<Notification, bool>> pred)
        {
            var response = await _context.Notifications.FirstOrDefaultAsync(pred);
            return response;
        }

        public async Task<ICollection<Notification>> GetSelectedAsync(Expression<Func<Notification, bool>> pred)
        {
            var response = await _context.Notifications.Where(pred).ToListAsync();
            return response;
        }

        public Notification Update(Notification notification)
        {
            _context.Notifications.Update(notification);
            return notification;
        }
    }
}
