using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpSQlite.Model;
using SharpSQlite.Model.Repository;
using Microsoft.AspNetCore.Authorization;
using SharpSQlite.AccountViewModels;

namespace SharpSQlite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository UserRepository = new UserRepository();

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UserRepository.CreateUser(model.Email, model.FirstName, model.LastName, model.DateOfBirth, model.Password, model.SecretQuestion);
                    return Redirect("VerificationSent");
                }
            }
            catch (Exception e)
            {
                AddErrors(e);
            }
            // something wrong, redisplay for edit
            return View(model);
        }

        public IActionResult VerificationSent()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        public IActionResult Recover()
        {
            return View();
        }

        public IActionResult Reset()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private void AddErrors(Exception error)
        {
           ModelState.AddModelError(string.Empty, error.Message);
        }
    }
}
