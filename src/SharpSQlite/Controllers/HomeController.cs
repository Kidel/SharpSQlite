using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpSQlite.Model;

namespace SharpSQlite.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();
        public IActionResult Index()
        {
            try
            {
                var category = new Category { Title = "great stuff", Description = "Category for great stuff", UrlSlug = "great_stuff" };
                _dbContext.Categories.Add(category);
                var categories = new List<Category>();
                categories.Add(category);
                var movie = new Movie { Name = "Great movie", Description = "This movie is Great", UrlSlug = "great_movie", Category = category };
                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();
                //var gotMovie = _dbContext.Movies.First();
                //return View(gotMovie);
            }
            catch (Exception e)
            {
                return View(new Movie());
            }
            return View(new Movie());
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
