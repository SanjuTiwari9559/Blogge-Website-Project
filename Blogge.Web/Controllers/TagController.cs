using Blogge.Web.Data;
using Blogge.Web.Models.Domain;
using Blogge.Web.Models.ViewModel;
using Blogge.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogge.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepositories tagRepositories;
        //private readonly BloggieDbContext bloggieDbContext;

        //public TagController(BloggieDbContext bloggieDbContext)
        //{
        //    this.bloggieDbContext = bloggieDbContext;
        //}
        public TagController(ITagRepositories tagRepositories)
        {
           this.tagRepositories = tagRepositories;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            await tagRepositories.AddAsync(tag);
            return RedirectToAction("List");


       }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        public  async Task<IActionResult> List()
        {
            //var list = tagRepositories.GetAllAsync();
            var tages = await tagRepositories.GetAllAsync();
            return View(tages);
        }

        //    return View(list);
        //}
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //var tags= bloggieDbContext.Tags.FindAsync(id);
            var tags = await tagRepositories.GetAsync(id);
            if (tags != null)
            {
                var editTagRequest = new EditTagRequest

                {
                    Id=tags.Id,
                   Name= tags.Name,
                   DisplayName= tags.DisplayName,

                };
                return View(editTagRequest);
            }

            return View();
            }








        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tags = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var update = await tagRepositories.UpdateAsync(tags);
            if (update != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View(editTagRequest);
            }
        }

        //    return RedirectToAction("List");

        //}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
            public async Task<IActionResult >Delete(EditTagRequest editTagRequest)
        {
            var tags = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            var deleted= await tagRepositories.DeleteAsync(tags.Id);
            if(deleted != null)
            {
                return RedirectToAction("List");
            }

            return View(editTagRequest);
        }
                

            //{
            //    return View(tag);
            //    //_dbContext.Tags.Remove(tag);
            //    //_dbContext.SaveChanges();   
            //    //return RedirectToAction("List");
            //}


    }
}


