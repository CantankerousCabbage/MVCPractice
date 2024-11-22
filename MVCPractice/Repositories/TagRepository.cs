using Microsoft.AspNetCore.Mvc;
using MVCPractice.Data;
using MVCPractice.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace MVCPractice.Repositories;

public class TagRepository : ITagRepository
{
    private readonly MvcDbContext mvcDbContext;

    public TagRepository(MvcDbContext mvcDbContext)
    {
        this.mvcDbContext = mvcDbContext;
    }

    async public Task<Tag> AddAsync(Tag tag)
    {
        await mvcDbContext.Tags.AddAsync(tag);
        await mvcDbContext.SaveChangesAsync();

        return tag;
    }

    public async Task<Tag?> DeleteAsync(Guid id)
    {
        var existingTag = await mvcDbContext.Tags.FindAsync(id);

        if (existingTag != null)
        {
            mvcDbContext.Tags.Remove(existingTag);
            await mvcDbContext.SaveChangesAsync();

            return existingTag;
        }

        return null;
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await mvcDbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetAsync(Guid id)
    {
        return await mvcDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Tag?> UpdateAsync(Tag tag)
    {
        var existingTag = await mvcDbContext.Tags.FindAsync(tag.Id);

        if (existingTag != null)
        {
            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;

            await mvcDbContext.SaveChangesAsync();

            return existingTag;
        }
        else
        {
            return null;
        }
    }
}