namespace HMSMVC.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
