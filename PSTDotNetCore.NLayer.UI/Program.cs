// See https://aka.ms/new-console-template for more information
using PSTDotNetCore.NLayer.BusinessLogic.Services;
using PSTDotNetCore.NLayer.DataAccess.Models;

Console.WriteLine("Hello, World!");

BL_Blog bL_Blog = new BL_Blog();
//bL_Blog.CreateBlog();

var lst = bL_Blog.GetBlogs();
foreach (BlogModel item in lst)
{
    Console.WriteLine(item.BlogId);
    Console.WriteLine(item.BlogTitle);
    Console.WriteLine(item.BlogAuthor);
    Console.WriteLine(item.BlogContent);
    Console.WriteLine("--------------------------");
}
