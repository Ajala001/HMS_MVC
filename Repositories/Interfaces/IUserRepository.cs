using HMSMVC.Entity;
using HMSMVC.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace HMSMVC.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        User Update (User user);
        void Delete(User user);
        Task<ICollection<User>> GetAllAsync();
        Task<User> GetAsync(Expression<Func<User, bool>> pred);
        Task<bool> IsExistAsync(string email);
    }
}
