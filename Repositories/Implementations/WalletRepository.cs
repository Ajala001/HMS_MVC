using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class WalletRepository(HMSDataContext _context) : IWalletRepository
    {
        public async Task<Wallet> AddAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            return wallet;
        }

        public void Delete(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
        }

        public async Task<ICollection<Wallet>> GetAllAsync()
        {
            return await _context.Wallets
                        .Include(a => a.Patient)
                        .ThenInclude(a => a.User)
                        .ToListAsync();  
        }

        public async Task<Wallet> GetAsync(Expression<Func<Wallet, bool>> pred)
        {
            var response = await _context.Wallets
                                 .Include(a => a.Patient)
                                 .ThenInclude(a => a.User)
                                 .FirstOrDefaultAsync(pred);
            return response;
        }

        public Wallet Update(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            return wallet;
        }
    }
}
