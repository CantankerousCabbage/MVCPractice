using Microsoft.EntityFrameworkCore;
using MVCPractice.Data;
using MVCPractice.Models.Domain;

namespace MVCPractice.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly MvcDbContext dbContext;

    public BlogPostRepository(MvcDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await dbContext.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();

        return blogPost;
    }

    public Task<BlogPost?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        // Include Tags from related Entity (specified by many to many relationship)
        return await dbContext.BlogPosts.Include( x => x.Tags).ToListAsync();
    }

    public async Task<BlogPost?> GetAsync(Guid id)
    {
        return await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var existingBlog = await dbContext.BlogPosts.Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (existingBlog != null)
        {
            existingBlog.Id = blogPost.Id;
            existingBlog.Heading = blogPost.Heading;
            existingBlog.PageTitle = blogPost.PageTitle;
            existingBlog.Content = blogPost.Content;
            existingBlog.ShortDescription = blogPost.ShortDescription;
            existingBlog.Author = blogPost.Author;
            existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlog.UrlHandle = blogPost.UrlHandle;
            existingBlog.Visible = blogPost.Visible;
            existingBlog.PublishedDate = blogPost.PublishedDate;
            existingBlog.Tags = blogPost.Tags;

            await dbContext.SaveChangesAsync();
            return existingBlog;
        }

        return null;
    }
}