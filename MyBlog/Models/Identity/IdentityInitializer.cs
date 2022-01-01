using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MyBlog.Models.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(m => m.Name.Equals("admin")))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "admin",
                    Description = "admin role"
                };
                manager.Create(role);
            }

            if (!context.Roles.Any(m => m.Name.Equals("user")))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "user",
                    Description = "user role"
                };
                manager.Create(role);
            }

            if (!context.Users.Any(m => m.Name.Equals("kadiryolcu")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Kadir",
                    Surname = "Yolcu",
                    UserName = "kdr123",
                    Email = "kadiryolcu57@gmail.com"
                };
                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");

            }

            if (!context.Users.Any(m => m.Name.Equals("huseyinyolcu")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Huseyin",
                    Surname = "Yolcu",
                    UserName = "hsyn123",
                    Email = "hsynyolcu57@gmail.com"
                };
                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "user");

            }


            base.Seed(context);
        }
    }
}