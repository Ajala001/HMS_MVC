using HMSMVC.Repositories.Interfaces;

namespace HMSMVC.Repositories.Implementations
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _environment;
        public FileRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var name = file.ContentType.Split('/');
            var newName = $"{name[0]}{Guid.NewGuid()}.{name[1]}";

            var directory = Path.Combine(_environment.WebRootPath, "Files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, newName);

            using (var filePath = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(filePath);
            }
            return path;
        }
    }
}
