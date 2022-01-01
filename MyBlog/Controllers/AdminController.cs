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
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            ViewBag.comments = db.Comments.Count();
            ViewBag.unconfirm = db.Articles.Where(m => m.confirm == false).Count();
            ViewBag.category = db.Categories.Count();
            ViewBag.confirm = db.Articles.Where(m => m.confirm == true).Count();
            ViewBag.sayi = db.Articles.Count();
            return View();
        }

        public ActionResult WriterList()
        {
            var list = db.Articles.ToList();
            return View(list);
        }

        public ActionResult ConfirmList()
        {
            var list = db.Articles.Where(m => m.confirm == true).ToList();
            return View(list);
        }

        public ActionResult UnconfirmList()
        {
            var list = db.Articles.Where(m => m.confirm == false).ToList();
            return View(list);
        }

        public ActionResult userComments(int page=1)
        {
            var commenst = db.Comments.ToList().ToPagedList(page,5);
            return View(commenst);
        }

        public ActionResult Delete(int id)
        {
            var dlt = db.Comments.Find(id);
            db.Comments.Remove(dlt);
            db.SaveChanges();
            return RedirectToAction("userComments");
        }



    }
}