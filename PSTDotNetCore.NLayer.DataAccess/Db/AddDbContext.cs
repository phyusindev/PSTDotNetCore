using Microsoft.EntityFrameworkCore;
using PSTDotNetCore.NLayer.DataAccess.Models;

namespace PSTDotNetCore.NLayer.DataAccess.Db
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
