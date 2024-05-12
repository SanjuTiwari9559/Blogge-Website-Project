using Blogge.Web.Data;
using Blogge.Web.Models.Domain;
using Blogge.Web.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Blogge.Web.Repositories
{
    public class TagRepositories : ITagRepositories
    {
        private readonly BloggieDbContext _dbContext;
        public TagRepositories(BloggieDbContext bloggieDbContext)
        {
           this. _dbContext = bloggieDbContext;
        }

      

        public  async Task<Tag> AddAsync(Tag tag)
        {

            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
          var Delete= await _dbContext.Tags.FindAsync(id);
            if(Delete != null)
            {
                _dbContext.Tags.Remove(Delete);
                _dbContext.SaveChanges();
                return Delete;
            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var list= await _dbContext.Tags.ToListAsync();
            return list;

        }

        public async Task<Tag> GetAsync(Guid id)
        { 
          return  await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public  async Task<Tag> UpdateAsync(Tag tag)
        {
           var existingTag = await _dbContext.Tags.FindAsync(tag.Id);
            if (existingTag!=null)
            {
                existingTag.Id= tag.Id;
                existingTag.Name=tag.Name;
                existingTag.DisplayName=tag.DisplayName;
               // _dbContext.Update(existingTag);

               await _dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;  
            
        }
    }
}
