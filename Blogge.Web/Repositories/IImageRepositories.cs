namespace Blogge.Web.Repositories
{
    public interface IImageRepositories
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
