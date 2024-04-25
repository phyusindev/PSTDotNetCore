﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        private readonly AddDbContext db = new AddDbContext();
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);
            //Create("Title", "Author", "Content");
            //Update(2004, "Title2", "Author2", "Content2");
            Delete(2004);
        }
        public void Read()
        {
            
            var lst = db.Blogs.ToList();
            foreach(BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------");
            }
        }
        public void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------------------");

        }
        public void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            db.Blogs.Add(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }
        public void Update(int id,string title,string author,string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result = db.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }

    }
}
