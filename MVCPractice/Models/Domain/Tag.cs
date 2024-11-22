namespace MVCPractice.Models.Domain;

using System.ComponentModel.DataAnnotations;

public class Tag {
    public Guid Id { get; set; }
    public required string Name  { get; set; }
    public required string DisplayName { get; set; }
    public ICollection<BlogPost>? BlogPosts { get; set; }
}