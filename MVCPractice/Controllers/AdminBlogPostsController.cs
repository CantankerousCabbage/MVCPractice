using Microsoft.AspNetCore.Mvc;

namespace MVCPractice.Controllers;

public class AdminBlogPostsController : Controller
{
        public IActionResult Add()
        {
            return View();
        }
}