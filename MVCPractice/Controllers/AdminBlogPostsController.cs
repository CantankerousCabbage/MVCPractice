using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPractice.Models.Domain;
using MVCPractice.Models.ViewModels;
using MVCPractice.Repositories;

namespace MVCPractice.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository tagRepository;
    private readonly IBlogPostRepository blogPostRepository;

    public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
    {
        this.tagRepository = tagRepository;
        this.blogPostRepository = blogPostRepository;
    }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get tags from repository
           var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // Map view model to domain model
            var blogPostDomainModel = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPostDomainModel);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogposts = await blogPostRepository.GetAllAsync();

            return View(blogposts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository
            var blogPostDomain = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if( blogPostDomain != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPostDomain.Id,
                    Heading = blogPostDomain.Heading,
                    PageTitle = blogPostDomain.PageTitle,
                    Content = blogPostDomain.Content,
                    ShortDescription = blogPostDomain.ShortDescription,
                    FeaturedImageUrl = blogPostDomain.FeaturedImageUrl,
                    UrlHandle = blogPostDomain.UrlHandle,
                    PublishedDate = blogPostDomain.PublishedDate,
                    Author = blogPostDomain.Author,
                    Visible = blogPostDomain.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name, Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPostDomain.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }
            //Map Domain Model into a view model.

            //Pass Data to View
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map view model back to domain model

            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Author = editBlogPostRequest.Author,
                Visible = editBlogPostRequest.Visible,
            };

            //Map Tags tags into domain model
            var selectedTags = new List<Tag>();

            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if ( foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if(updatedBlog != null)
            {
                //Show success notification
                return RedirectToAction("Edit");
            }

            //Show failure notification
            return RedirectToAction("Edit");
        }
}