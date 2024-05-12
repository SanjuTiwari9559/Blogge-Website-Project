using Blogge.Web.Data;
using Blogge.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogge.Web.Repositories
{
    public class BlogPostRepositories : IBlogPostRepositories
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepositories(BloggieDbContext bloggieDbContext)
        {

            this.bloggieDbContext = bloggieDbContext;
        }



        public async Task<BlogPost> AddAsyanc(BlogPost blogPost)
        {
            await bloggieDbContext.AddAsync(blogPost);
            bloggieDbContext.SaveChanges();
            return blogPost;
        }

        public async Task<BlogPost> DeleteAsyanc(Guid id)
        {
            var Deleted = await bloggieDbContext.Posts.FindAsync(id);
            if (Deleted != null)
            {
                
                bloggieDbContext.Posts.Remove(Deleted);
                await bloggieDbContext.SaveChangesAsync();
                return Deleted;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.Posts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsyanc(Guid id)
        {
            return await bloggieDbContext.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsyanc(string urlHandle)
        {
           return await bloggieDbContext.Posts.Include(x=>x.Tags).FirstOrDefaultAsync(x=> x.UrlHandle== urlHandle);
        }

        public async Task<BlogPost> UpdateAsyanc(BlogPost post)
        {
            var existing = await bloggieDbContext.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == post.Id);
            if (existing != null)
            {
                existing.Id = post.Id;
                existing.Heading = post.Heading;
                existing.Author = post.Author;
                existing.PageTitle = post.PageTitle;
                existing.Content = post.Content;
                existing.ShortDescription = post.ShortDescription;
                existing.FeaturedImageUrl = post.FeaturedImageUrl;
                existing.PublishedDate = post.PublishedDate;
                existing.Visible = post.Visible;
                existing.Tags = post.Tags;
                await bloggieDbContext.SaveChangesAsync();
                return existing;




            }
            else
            {
                return null;
            }

        }

    }
}
