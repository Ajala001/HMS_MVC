using HMSMVC.Data;
using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using HMSMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HMSDataContext _context;
        public UserRepository(HMSDataContext context) 
        { 
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> pred)
        {
            var response = await _context.Users.FirstOrDefaultAsync(pred);
            return response;
        }

        public async Task<bool> IsExistAsync(string email)
        {
            var response = await _context.Users.AnyAsync(x => x.Email == email);
            return response;
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }

    }
}
