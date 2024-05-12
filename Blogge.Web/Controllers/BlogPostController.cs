using Blogge.Web.Data;
using Blogge.Web.Models.Domain;
using Blogge.Web.Models.ViewModel;
using Blogge.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
namespace Blogge.Web.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly ITagRepositories tagRepositories;

        public IBlogPostRepositories BlogPostRepositories { get; }

        public BlogPostController(ITagRepositories tagRepositories,IBlogPostRepositories blogPostRepositories)
        {
            this.tagRepositories = tagRepositories;
            BlogPostRepositories = blogPostRepositories;
        }
        [Authorize( Roles ="Admin")]
        [HttpGet]
        public async Task< IActionResult >Add()
        {
            var taglist =  await tagRepositories.GetAllAsync();

            var tags = new AddBlogPostRequest
            {
                ListItems = taglist.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(tags);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult >Add(AddBlogPostRequest addBlogPost)
        {
            var post = new BlogPost
            {
                Heading = addBlogPost.Heading,
                PageTitle = addBlogPost.PageTitle,
                Content = addBlogPost.Content,
                ShortDescription = addBlogPost.ShortDescription,
                FeaturedImageUrl = addBlogPost.FeaturedImageUrl,
                UrlHandle = addBlogPost.UrlHandle,
                PublishedDate = addBlogPost.PublishedDate,
                Author=addBlogPost.Author,
                Visible = addBlogPost.Visible,
                          
                 

            };

            //Map ListItem to selectedTag
            var selectedTag = new List<Tag>();
            foreach(var SeletedTagId in addBlogPost.SelectedTags)
            {
                var SelectedTagIdAsGuid = Guid.Parse(SeletedTagId);
           var exitingTag=     await tagRepositories.GetAsync(SelectedTagIdAsGuid);
                selectedTag.Add(exitingTag);

            }
            //This is ADD TO Tags of BlogPost 
            post.Tags=selectedTag;
             await BlogPostRepositories.AddAsyanc(post);
            return RedirectToAction("Add");


        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
           var existingPost=  await BlogPostRepositories.GetAllAsync();
            //var posts=bloggieDbContext.Posts.ToList();
            return View(existingPost);
        }
        //Map ListItem to selectedTag
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await BlogPostRepositories.GetAsyanc(id);
            var tagDomainModel = await tagRepositories.GetAllAsync();
            if (post != null) {
                var blogPost = new EditBlogPostRequest
                {
                    Id = post.Id,
                    Heading = post.Heading,
                    PageTitle = post.PageTitle,
                    Content = post.Content,
                    ShortDescription = post.ShortDescription,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    UrlHandle = post.UrlHandle,
                    PublishedDate = post.PublishedDate,
                    Author = post.Author,
                    Visible = post.Visible,
                    ListItems=tagDomainModel.Select( x=>new SelectListItem
                    {
                        Text=x.Name,
                        Value=x.Id.ToString()
                    }),
                   SelectedTags= post.Tags.Select(x =>x.Id.ToString()).ToArray(),

                   
        };
      
                return View(blogPost);

            }
            else
            {
                return RedirectToAction("Edit");
            }

           
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {

            var post = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Author = editBlogPostRequest.Author,
                Visible = editBlogPostRequest.Visible,


            };
            var seletedTag = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var Tag))
                {
                    var foundTag = await tagRepositories.GetAsync(Tag);
                    if (foundTag != null)
                    {
                        seletedTag.Add(foundTag);
                    }
                }
            }
            post.Tags = seletedTag;
            var updated = await BlogPostRepositories.UpdateAsyanc(post);
            if (updated != null)
            {
                return RedirectToAction("List");

            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
   public async Task<IActionResult>Delete(EditBlogPostRequest editBlogPostRequest)
        { 
            var deleted= await BlogPostRepositories.DeleteAsyanc(editBlogPostRequest.Id);
            if(deleted != null)
            {
                return RedirectToAction("List");
            }
            else
            { 
                return RedirectToAction("Edit");
            }
            
        }
            
        }
    }

