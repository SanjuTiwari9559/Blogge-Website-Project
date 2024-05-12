
using Blogge.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogge.Web.Repositories
{

    public class BlogPostLikeRepositories : IBlogPostLikeRepositories
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepositories(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

       

        public async Task<int> GetTotalLikes(Guid blogPostid)
        {
           return await bloggieDbContext.BlogPostLikes.CountAsync(x =>x.BlogPostId == blogPostid);
        }
    }
}
