using Blogge.Web.Models.ViewModel;
using Blogge.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogge.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepositories blogPostRepositories;
        private readonly IBlogPostLikeRepositories blogPostLikeRepositories;

        public BlogsController(IBlogPostRepositories blogPostRepositories,IBlogPostLikeRepositories blogPostLikeRepositories)
        {
            this.blogPostRepositories = blogPostRepositories;
            this.blogPostLikeRepositories = blogPostLikeRepositories;
        }
        [HttpGet]
        public  async Task<IActionResult >Index(string urlHandle)
        {
            var UrlImformation= await blogPostRepositories.GetByUrlHandleAsyanc(urlHandle);
            var blogDetails=new BlogDetails();
           
            if(UrlImformation != null) {
                var totallike = await blogPostLikeRepositories.GetTotalLikes(UrlImformation.Id);
                   blogDetails = new BlogDetails
                {
                    Id = UrlImformation.Id,
                    Heading = UrlImformation.Heading,
                    Content = UrlImformation.Content,
                    ShortDescription = UrlImformation.ShortDescription,
                    FeaturedImageUrl = UrlImformation.FeaturedImageUrl,
                    UrlHandle = UrlImformation.UrlHandle,
                    PublishedDate = UrlImformation.PublishedDate,
                    Author = UrlImformation.Author,
                    Tags = UrlImformation.Tags,
                    totalLikes = totallike
                };
            }
            return View(blogDetails);
        }
    }
}
