using Microsoft.EntityFrameworkCore;
using PSTDotNetCore.RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetCore.RestApi.Db
{
    internal class AddDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
