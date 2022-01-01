using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Category
    {
        public short Id { get; set; }
        public string CategoryName { get; set; }

        public virtual List<Article>Articles { get; set; }
    }
}