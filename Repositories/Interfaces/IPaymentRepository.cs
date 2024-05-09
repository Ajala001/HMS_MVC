using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> AddAsync(Payment payment);
        Payment Update(Payment payment);
        void Delete(Payment payment);
        Task<ICollection<Payment>> GetAllAsync();
        Task<Payment> GetAsync(Expression<Func<Payment, bool>> pred);
        Task<ICollection<Payment>> GetSelectedAsync(Expression<Func<Payment, bool>> pred);
    }
}
