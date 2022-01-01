using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class ArticleController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Article
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var articlelist = db.Articles.Where(m=>m.userName==username).Select(m => new ArticleModel()
            {
                Id = m.Id,
                Title = m.Title,
                userName = m.userName,
                Image = m.Image,
                Date = m.Date,
                confirm = m.confirm,
                Views = m.Views,
                Comments = m.Comments,
                Description = m.Description.Length > 20 ? m.Description.Substring(0, 20) + ("[...]") : m.Description
            }
            ).ToList();
            return View(articlelist);
        }

        // GET: Article/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Article article, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                article.userName = User.Identity.Name;
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extn = Path.GetExtension(Request.Files[0].FileName);
                string url = "/Upload/Images/" + filename + extn;
                Request.Files[0].SaveAs(Server.MapPath(url));
                article.Image = "/Upload/Images/" + filename + extn;
                db.Articles.Add(article);
                db.SaveChanges();
                if (this.User.Identity.Name=="admin")
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.Message = "Makaleniz onaylandıktan sonra yayıma alınacaktır,teşekkürler...";
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", article.CategoryId);
            return View(article);
        }

        // GET: Article/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", article.CategoryId);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,userName,Title,Image,Description,Date,Views,confirm,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", article.CategoryId);
            return View(article);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
