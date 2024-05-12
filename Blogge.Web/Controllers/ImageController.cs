using Blogge.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blogge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepositories imageRepositories;

        public ImageController(IImageRepositories imageRepositories)
        {
            this.imageRepositories = imageRepositories;
        }
        [HttpPost]
        public async Task< IActionResult >UploadAsync(IFormFile file)
            
        {
          var ImageUrl =await imageRepositories.UploadAsync(file);
            if(ImageUrl == null)
            {
                return Problem("Something went wrong",null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link=ImageUrl });
        }
    }
}
