using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogge.Web.Models.ViewModel
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        //Display Tag
        public IEnumerable<SelectListItem> ListItems { get; set; }
        //Collect Tag
        public string[] SelectedTags { get; set; } = Array.Empty<String>();
    }
}
