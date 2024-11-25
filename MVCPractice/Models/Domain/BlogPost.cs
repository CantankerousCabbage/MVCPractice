namespace MVCPractice.Models.Domain;

using System.ComponentModel.DataAnnotations;

public class BlogPost
{
    public Guid Id { get; set; }
    public required string Heading { get; set; }
    public required string PageTitle { get; set; }
    public required string Content { get; set; }
    public required string ShortDescription { get; set; }
    public required string FeaturedImageUrl { get; set; }
    public required string UrlHandle { get; set; }
    public DateTime PublishedDate { get; set; }
    public required string Author { get; set; }
    public bool Visible { get; set; }

    // Naviation Property
    public ICollection<Tag>? Tags { get; set; }
}