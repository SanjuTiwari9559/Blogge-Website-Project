namespace Blogge.Web.Repositories
{
    public interface IBlogPostLikeRepositories
    {
        Task<int> GetTotalLikes(Guid blogPostid);
    }
}
