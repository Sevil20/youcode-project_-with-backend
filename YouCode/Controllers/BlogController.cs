using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YouCode.Models.Context;
using YouCode.Models.Entity;



namespace YouCode.Controllers
{
    public class BlogController : Controller
    {

        private readonly YouCodeContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public BlogController(IWebHostEnvironment environment, YouCodeContext dbContext)
        {
            _dbContext = dbContext;
            _environment = environment; 
        }


        public IActionResult Index()
        {
            var blogs = _dbContext.Blogs.ToList();
            return View(blogs);
        }

        public IActionResult BlogDetail(int id)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.Id == id);

            if (blog == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(blog); // Make sure the model is passed to the view
        }


        //Create Blog
        public IActionResult CreateBlog()
        {
            return View();
        }

        /*        [HttpPost]
        */
        /*public IActionResult Create(Blog blog, string blogName, string blogDescription, string blogImage)
        {
            var newBlog = new Blog
            {
                BlogName = blogName,
                CreatedDate = DateTime.Now,
                BlogDescription = blogDescription,
                BlogImage = blogImage
                // Set other properties as needed
            };

            // Save the new blog to the database
            _dbContext.Blogs.Add(newBlog);
            _dbContext.SaveChanges();

            return Json(new { Success = true });


        }*/

        [HttpPost]
        public IActionResult Create(string blogName, string blogDescription, IFormFile blogImage)
        {
            // Create a new Blog entity
            var newBlog = new Blog
            {
                BlogName = blogName,
                CreatedDate = DateTime.Now,
                BlogDescription = blogDescription,
                // Set other properties as needed
            };
            if (blogImage != null)
            {
                string imagePath = Path.Combine(_environment.WebRootPath, "uploads", blogImage.FileName);
                using var stream = new FileStream(imagePath, FileMode.Create);
                blogImage.CopyTo(stream);
                newBlog.BlogImage = imagePath;
            }

            // Save the new blog to the database
            _dbContext.Blogs.Add(newBlog);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
