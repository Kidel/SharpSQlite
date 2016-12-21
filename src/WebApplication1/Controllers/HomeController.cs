﻿using System;
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
                var category = _dbContext.Categories.First();
                return View(category);
            }
            catch (Exception e)
            {
                return View(new Category());
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
