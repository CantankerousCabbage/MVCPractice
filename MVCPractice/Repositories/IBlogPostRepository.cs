using MVCPractice.Models.Domain;

namespace MVCPractice.Repositories;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();

    Task<BlogPost?> GetAsync(Guid id);
    Task<BlogPost> AddAsync(BlogPost BlogPost);
    Task<BlogPost?> UpdateAsync(BlogPost BlogPost);
    Task<BlogPost?> DeleteAsync(Guid id);
}
