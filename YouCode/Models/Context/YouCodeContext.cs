using Microsoft.EntityFrameworkCore;
using YouCode.Models.Entity;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Configuration;

namespace YouCode.Models.Context
{
    public class YouCodeContext : DbContext
    {
        internal object Project;

        public YouCodeContext(DbContextOptions dbContext): base(dbContext)
        { 

        }
      
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Blog> Blogs { get; set; }


        // Other service configurations...
        public object? project { get;  set; }
        public object Language { get; internal set; }
    }
}
