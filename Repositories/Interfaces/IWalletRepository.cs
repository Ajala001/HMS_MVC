using HMSMVC.Entity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet> AddAsync(Wallet wallet);
        Wallet Update(Wallet wallet);
        void Delete(Wallet wallet);
        Task<ICollection<Wallet>> GetAllAsync();
        Task<Wallet> GetAsync(Expression<Func<Wallet, bool>> pred);
    }
}
