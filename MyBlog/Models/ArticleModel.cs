using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Models
{
    public class ArticleModel
    {
        public short Id { get; set; }
        public string userName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [UIHint("tinymce_full_compressed"), AllowHtml]

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Views { get; set; }
        public bool confirm { get; set; }

        public string CategoryName { get; set; }



        public virtual ICollection<Comment> Comments { get; set; }
    }
}