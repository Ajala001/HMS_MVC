namespace HMSMVC.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
