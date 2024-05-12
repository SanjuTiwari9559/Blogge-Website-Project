using Blogge.Web.Models;
using Blogge.Web.Models.ViewModel;
using Blogge.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBlogPostRepositories BlogPostRepositories;
        private readonly ITagRepositories tagRepositories;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepositories blogPostRepositories,ITagRepositories tagRepositories)
        {
            _logger = logger;
            BlogPostRepositories = blogPostRepositories;
            this.tagRepositories = tagRepositories;
        }

        public async Task<IActionResult >Index()
        {
           var allBlogPost= await BlogPostRepositories.GetAllAsync();
            var tags=await tagRepositories.GetAllAsync();
            var viewMdel = new HomeViewModel
            {
                Posts = allBlogPost,
                Tags = tags
            };
            return View(viewMdel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
