using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSTDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using PSTDotNetCore.Shared;

namespace PSTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "Select * from Tbl_Blog where BlogId = @BlogId";


            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

 
            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent));

        
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

       

    }
}
