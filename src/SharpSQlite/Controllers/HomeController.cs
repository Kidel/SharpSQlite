using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpSQlite.Model;
using SharpSQlite.Model.Repository;

namespace SharpSQlite.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogRepository BlogRepository = new BlogRepository();
        private readonly PostAggregate PostAggregate = new PostAggregate();
        private readonly UserRepository UserRepository = new UserRepository();

        public IActionResult Index()
        {
            var lastPost = PostAggregate.GetMostRecentPost();
            ViewData["Error"] = (lastPost == null);
            return View(lastPost);
        }

        public IActionResult Populate()
        {
            if(PostAggregate.CreatePost("A Post", "a_post", "This is a post", 
                    new List<string> { "cool", "good" },
                    BlogRepository.CreateBlog("A Blog").BlogId, 
                    UserRepository.CreateUser("mario.rossi@notarealmail.fake", "Mario", "Rossi", "6 Sep 1987", "pepette", "Cosa pippa la pappa?").UserId
                ) == null) ViewData["Error"] = "Error while creating data";
            else ViewData["Error"] = "";
            return View();
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
