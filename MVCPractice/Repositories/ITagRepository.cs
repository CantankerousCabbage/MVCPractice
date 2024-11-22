using MVCPractice.Models.Domain;

namespace MVCPractice.Repositories;
public interface ITagRepository
{

    Task<IEnumerable<Tag>> GetAllAsync();

    Task<Tag?> GetAsync(Guid id);

    Task<Tag> AddAsync(Tag tag);

    //Can be null because it checks if it exists before updating
    Task<Tag?> UpdateAsync(Tag tag);

    Task<Tag?> DeleteAsync(Guid id);
}