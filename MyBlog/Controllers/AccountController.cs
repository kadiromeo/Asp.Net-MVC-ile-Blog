using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MyBlog.Models;
using MyBlog.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);

        }

        public ActionResult LogOut()
        {
            var autManager = HttpContext.GetOwinContext().Authentication;
            autManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register p)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser();
                user.Name = p.Name;
                user.Surname = p.Surname;
                user.UserName = p.userName;
                user.Email = p.Email;
                var result = UserManager.Create(user, p.Password);

                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.Message = "Kullanıcı adı ve Email kullanılmaktadır...!";
                }


            }

            return View(p);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login p,string ReturnUrl)
        {
            /*
            var login = UserManager.Find(p.userName, p.Password);
            if (login!=null)
            {
                Session["userId"] = login.Id;
                Session["username"] = login.UserName;
                Session["role"] = login.Roles;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }*/

            /*
            var user = UserManager.Find(p.userName, p.Password);
            if (user!=null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                Session["userName"] = user.UserName.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "username or password incorrect";

            }
                          */

            if (ModelState.IsValid)
            {
                var user = UserManager.Find(p.userName, p.Password);
                if (user != null)
                {
                    var autManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var autProperties = new AuthenticationProperties();
                    autProperties.IsPersistent = p.RememberMe;
                    autManager.SignIn(autProperties, identityclaims);
                    if (ReturnUrl==null)
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }

                }
                else
                {
                    ViewBag.Message = "Kullanıcı adı veya şifre yanlış...!";
                }
            }



            return View(p);
        }

        public ActionResult Profil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            var data = new ProfileChange()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                userName = user.UserName
            };
            return View(data);
        }

        [HttpPost]
        public ActionResult Profil(ProfileChange p)
        {

            var user = UserManager.FindById(p.Id);
            if (user != null)
            {
                user.Name = p.Name;
                user.Surname = p.Surname;
                user.Email = p.Email;
                user.UserName = p.userName;
                UserManager.Update(user);
                ViewBag.Message = "Bilgileriniz Başarılı bir şekilde güncellenmiştir...!";
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var use = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                ViewBag.Message = "Şifreniz Başarılı Bir Şekil Değiştirilmiştir...!";
            }
            return View(model);
        }



    }
}