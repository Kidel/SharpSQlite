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
using SharpSQlite.Util;

namespace SharpSQlite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository UserRepository = new UserRepository();

        private IEmailSender _emailSender { get; }

        public AccountController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UserRepository.GetUserByEmailPassword(model.Email, model.Password);
                    if (user.Verified)
                    {
                        UserSessionManager.Set(HttpContext, user.UserId);
                        return Redirect("../");
                    }
                    else
                        return Redirect("VerificationSent");
                }
            }
            catch (Exception e)
            {
                AddErrors(e.Message);
            }
            // something wrong, redisplay for edit
            return View(model);
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

                    string mailText = $"Hello {user.FirstName}, \n This is your verification link \nhttp://sharpsqlite.azurewebsites.net/Account/VerifyEmail/{user.Email}/{user.VerificationCode} \nRegards, \n SharpSQlite";

                    //_emailSender.SendEmailAsync(user.Email, "Verify your email address", mailText, user.FirstName);

                    // Development
                    ViewData["Message"] = "This demo host doesn't have a SMTP server (there is the code however), so the text of the email is reported here instead: " + mailText;

                    return View("VerificationSent");
                }
            }
            catch (Exception e)
            {
                AddErrors(e.Message);
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
            UserSessionManager.Remove(HttpContext);
            return View();
        }

        public IActionResult VerifyEmail(string id, string data)
        {
            try
            {
                var user = UserRepository.GetUserByEmail(id);
                if (user != null && user.VerificationCode == data)
                {
                    user.Verified = true;
                    UserRepository.UpdateUser(user);
                    return Redirect("../../AccountVerified");
                }
                else
                {
                    AddErrors("Invalid code");
                    return Redirect("../../Error");
                }
            }
            catch (Exception e)
            {
                AddErrors(e.Message);
                return Redirect("../../Error");
            }
        }

        public IActionResult AccountVerified()
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

        private void AddErrors(string error)
        {
            ModelState.AddModelError(string.Empty, error);
        }
    }
}
