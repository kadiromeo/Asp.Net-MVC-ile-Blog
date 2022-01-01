using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace MyBlog.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var category = new List<Category>()
            {
              new Category() {CategoryName="Php"},
              new Category() {CategoryName="C#"},
              new Category() {CategoryName="Java"}
            };
            foreach (var item in category)
            {
                context.Categories.Add(item);
            }
            context.SaveChanges();


            var article = new List<Article>()
            {
              new Article() {
                Title="epfaweofwejfaeopwfwefew",
                Description="ofgerpogjseropgrjgeorg",
                Image="a1.png",
                Date=Convert.ToDateTime("2021-12-21"),
                Views=0,
                confirm=true,
                CategoryId=1,
                userName="kdryolcu",
              },


               new Article() {
                Title="epfaweofwejfaeopwfwefew",
                Description="ofgerpogjseropgrjgeorg",
                Image="a2.png",
                Date=Convert.ToDateTime("2021-12-21"),
                Views=0,
                confirm=true,
                CategoryId=2,
                userName="kdryolcu",
              },

                new Article() {
                Title="epfaweofwejfaeopwfwefew",
                Description="ofgerpogjseropgrjgeorg",
                Image="a1.png",
                Date=Convert.ToDateTime("2021-12-21"),
                Views=0,
                confirm=true,
                CategoryId=3,
                userName="kdryolcu",
              }

            };
            foreach (var item in article)
            {
                context.Articles.Add(item);
            }
            context.SaveChanges();





            base.Seed(context);
        }
    }
}