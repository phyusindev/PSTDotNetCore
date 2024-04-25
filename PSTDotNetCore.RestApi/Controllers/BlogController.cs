using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PSTDotNetCore.RestApi.Db;
using PSTDotNetCore.RestApi.Models;



namespace PSTDotNetCore.RestApi.Controllers
{
    //https://localhost:3000 => Domain Url
    //api/blog => endpoint

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AddDbContext _context;
        public BlogController()
        {
            _context = new AddDbContext();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x=>x.BlogId == id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            int result = _context.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null )
            {
                return NotFound("No Data Found.");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = _context.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            var result = _context.SaveChanges();

            string message = result > 0 ? "Patching Successful." : "Patching Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            _context.Blogs.Remove(item);
            var result = _context.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }
}
