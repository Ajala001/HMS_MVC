using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class PaymentRepository(HMSDataContext _context) : IPaymentRepository
    {
        public async Task<Payment> AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return payment;
        }

        public void Delete(Payment payment)
        {
            _context.Payments.Remove(payment);
        }

        public async Task<ICollection<Payment>> GetAllAsync()
        {
            return await _context.Payments
                         .Include(p => p.Patient)
                         .ThenInclude(p => p.User)
                         .ToListAsync();
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> pred)
        {
            var response = await _context.Payments
                                 .Include(p => p.Service)
                                 .Include(p => p.Patient)
                                 .ThenInclude(p => p.User)
                                 .FirstOrDefaultAsync(pred);
            return response;
        }

        public async Task<ICollection<Payment>> GetSelectedAsync(Expression<Func<Payment, bool>> pred)
        {
            return await _context.Payments
                         .Include(p => p.Service)
                         .Include(p => p.Patient)
                         .ThenInclude(p => p.User)
                         .Where(pred).ToListAsync();
        }

        public Payment Update(Payment payment)
        {
            _context.Payments.Update(payment);
            return payment;
        }
    }
}
