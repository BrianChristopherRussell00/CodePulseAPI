using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; set; }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await DbContext.BlogPosts.AddAsync(blogPost);
            await DbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {   
var existingBlogPost = await DbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlogPost != null)   
            {   
            DbContext.BlogPosts.Remove(existingBlogPost);
                await DbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await DbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await DbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await DbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }
            //Update BlogPost
            DbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            //Update Categories
            existingBlogPost.Categories = blogPost.Categories;

            await DbContext.SaveChangesAsync();

            return blogPost;

        }
    }

}
