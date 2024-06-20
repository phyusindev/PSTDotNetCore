using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSTDotNetCore.MvcApp.Db;
using PSTDotNetCore.MvcApp.Models;

namespace PSTDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _db.Blogs.ToListAsync();
            return View(lst);
        }

        [ActionName("Create")]
        public async Task<IActionResult> BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogCreate(BlogModel blog)
        {
            await _db.Blogs.AddAsync(blog);
            var result = await _db.SaveChangesAsync();
            //return View("BlogCreate");
            //return Redirect("/Blog");
            return RedirectToAction("Index");
        }
    }
}
