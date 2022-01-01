using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Comment
    {
        public short Id { get; set; }
        public string Description { get; set; }
        public string userId { get; set; }
        public DateTime Date { get; set; }
        public float score { get; set; }
        public short articleId { get; set; }

        public virtual Article Article { get; set; }

    }
}