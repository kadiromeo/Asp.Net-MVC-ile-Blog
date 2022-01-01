using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using PagedList;
using PagedList.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index(int page =1)
        {
            var articlelist = db.Articles.Where(m => m.confirm == true).Select(m => new ArticleModel()
            {
                Id = m.Id,
                Title = m.Title,
                userName = m.userName,
                Image = m.Image,
                Date = m.Date,
                confirm = m.confirm,
                Views = m.Views,
                Comments = m.Comments,
                Description = m.Description.Length > 60 ? m.Description.Substring(0, 60) + ("[...]"):m.Description
            }
            ).ToList().ToPagedList(page,5);
            return View(articlelist);
        }

        public ActionResult Detail(int id)
        {
            var result = (from sub in db.Comments
                          where sub.articleId == id
                          select sub.score

                        ).DefaultIfEmpty(0).Average();

            ViewBag.sub = Math.Round(result);

            var article = db.Articles.Find(id);
            ViewBag.Article = article;

            var views = db.Articles.ToList().Find(m => m.Id == id);
            views.Views += 1;
            db.SaveChanges();

            ViewBag.sayi = db.Comments.ToList().Where(m => m.articleId == id).Count();


            var comment = new Comment()
            {
                articleId = article.Id
            };

            return View("Detail",comment);
        }

        public ActionResult CommendSend(Comment comment, int rating)
        {
            comment.userId = User.Identity.Name;
            comment.Date = DateTime.Now;
            comment.score = Convert.ToInt32(rating);
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Detail", "Home", new { id = comment.articleId });
        }


        public ActionResult ArticleList(int ? id)
        {
            var article = db.Articles.Where(m => m.confirm == true).AsQueryable();
            if (id!=null)
            {
                article = article.Where(m => m.CategoryId == id);
            }
            return View(article.ToList());
        }

        public ActionResult Search(string veriable)
        {
            var search = db.Articles.Where(m => m.confirm == true && m.Title.Contains(veriable)).ToList();
            return View(search);
        }

        public PartialViewResult mostView()
        {
            var mostView = db.Articles.Where(m => m.confirm == true).OrderByDescending(m => m.Views).Take(5).ToList();
            return PartialView(mostView);
        }



    }
}