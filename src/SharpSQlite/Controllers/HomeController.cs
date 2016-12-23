using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpSQlite.Model;
using Microsoft.EntityFrameworkCore;

namespace SharpSQlite.Controllers
{
    public class Results<T>
    {
        public int Error { get; set; }
        public T Data { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();
        public IActionResult Index()
        {
            try
            {
                var firstPost = _dbContext.Posts.Include(post => post.Blog).First();
                return View(new Results<Post> { Error = 0, Data = firstPost });
            }
            catch(System.InvalidOperationException)
            {
                return View(new Results<Post> { Error = 1, Data = null });
            }

        }

        public IActionResult Populate()
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                ViewData["Error"] = "";
                try
                {
                    var blog = new Blog { Name = "A Blog" };
                    var user = new User { FirstName = "Mario", LastName = "Rossi" };
                    var post = new Post { Title = "A Post", Slug = "A Slug", Content = "This is a post", Blog = blog, Author = user };
                    var tag1 = new Tag { Name = "good" };
                    var tag2 = new Tag { Name = "cool" };
                    var postTag1 = new PostTag { Tag = tag1, Post = post };
                    var postTag2 = new PostTag { Tag = tag2, Post = post };
                    _dbContext.Blogs.Add(blog);
                    _dbContext.Posts.Add(post);
                    _dbContext.Tags.Add(tag1);
                    _dbContext.Tags.Add(tag2);
                    _dbContext.PostTags.Add(postTag1);
                    _dbContext.PostTags.Add(postTag2);
                    _dbContext.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.ToString();
                }

                return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
