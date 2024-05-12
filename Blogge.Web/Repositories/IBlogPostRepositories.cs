using Blogge.Web.Models.Domain;

namespace Blogge.Web.Repositories
{
    public interface IBlogPostRepositories
    {
        public Task<IEnumerable<BlogPost>> GetAllAsync();
        public Task<BlogPost ?> GetAsyanc(Guid id);
        public Task<BlogPost> AddAsyanc(BlogPost blogPost);
        public Task<BlogPost> UpdateAsyanc(BlogPost post);
        public Task<BlogPost> DeleteAsyanc(Guid id);
        public  Task<BlogPost?> GetByUrlHandleAsyanc(string urlHandle);
    }
}

