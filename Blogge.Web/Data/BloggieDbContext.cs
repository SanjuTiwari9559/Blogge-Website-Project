using Blogge.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogge.Web.Data
{
    public class BloggieDbContext:DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {

        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<BlogPostLike>BlogPostLikes { get; set; }
    }
}
