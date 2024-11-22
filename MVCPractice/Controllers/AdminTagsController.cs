using Microsoft.AspNetCore.Mvc;
using MVCPractice.Data;
using MVCPractice.Models.ViewModels;
using MVCPractice.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MVCPractice.Repositories;

namespace MVCPractice.Controllers;

public class AdminTagsController : Controller
{
    // private readonly MvcDbContext _MvcDbContext;
    private readonly ITagRepository tagRepository;

    //Constructor injection to get context

    public AdminTagsController( ITagRepository tagRepository)
    {
        // _MvcDbContext = mvcDbContext;
        this.tagRepository = tagRepository;
    }

    //Automatically looks for view corresponding to name of action method.
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Add")]
    async public Task<IActionResult> Add(AddTagRequest addTagRequest)
    {

        //Mapping AddTagRequest to the Tag domain model
        var tag = new Tag
        {
            Name= addTagRequest.Name,
            DisplayName= addTagRequest.DisplayName
        };

        await tagRepository.AddAsync(tag);
        //Specify add view to render
        return RedirectToAction("List");
    }

    [HttpGet]
    [ActionName("List")]
    async public Task<IActionResult> List()
    {
        //use dbContext to read the tags
        var tags = await tagRepository.GetAllAsync();

        return View(tags);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //First method for getting taf
        // var tag = _MvcDbContext.Tags.Find(id);

        // 2nd method
        var tag = await tagRepository.GetAsync(id);

        if (tag != null)
        {
            var editTagRequest = new EditTagRequest
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            return View(editTagRequest);
        }

        return View(null);
    }

    [HttpPost]
    async public Task<IActionResult> Edit(EditTagRequest editTagRequest)
    {
        var tag = new Tag
        {
            Id = editTagRequest.Id,
            Name = editTagRequest.Name,
            DisplayName = editTagRequest.DisplayName
        };

        var updatedTag = await tagRepository.UpdateAsync(tag);

        if (updatedTag != null)
        {
            // Show success notification
        }
        else
        {
            // Show fail notification
        }


        return RedirectToAction("Edit", new { id = editTagRequest.Id});
    }

    [HttpPost]
    async public Task<IActionResult> Delete(EditTagRequest editTagRequest)
    {
        var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

        if (deletedTag != null)
        {
            // Show success notification
            return RedirectToAction("List");
        }

        //Show an error notification if fails
        return RedirectToAction("Edit", new { id = editTagRequest.Id});
    }
}