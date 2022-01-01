using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyBlog.Models
{
    public class DataContext:DbContext
    {
        public DataContext():base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Article>Articles { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Comment>Comments { get; set; }
    }
}