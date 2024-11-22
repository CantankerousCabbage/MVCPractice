using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MVCPractice.Models.Domain;

namespace MVCPractice.Data;

public class MvcDbContext : DbContext
{
    public MvcDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }
}