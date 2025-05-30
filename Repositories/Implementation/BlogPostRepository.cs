using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;

namespace CodePulseAPI.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await DbContext.BlogPosts.AddAsync(blogPost);
            await DbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}
