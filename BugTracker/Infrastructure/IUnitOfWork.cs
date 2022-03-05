namespace BugTracker.Infrastructure
{
    public interface IUnitOfWork
    {
        void UploadImage(IFormFile file);
    }
}
