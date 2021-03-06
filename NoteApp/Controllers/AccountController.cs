﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using NoteApp.Files;
using NoteApp.Models;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController(UserRepository userRepository, IFileProvider[] fileProviders) : 
            base(userRepository, fileProviders)
        {
        }

        [AllowAnonymous]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(SigninViewModel model)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var result = SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false).Result;
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("", "Аккаунт заблокирован");
                        break;
                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                        break;
                    case SignInStatus.RequiresVerification:
                        ModelState.AddModelError("", "Требуется дополнительная верификация");
                        break;
                }
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Signout()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Signup()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignupViewModel model)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var user = new User(model.Login);
                var result = UserManager.CreateAsync(user, model.Password);
                if (result.Result.Succeeded)
                {
                    UserManager.AddToRoleAsync(user.Id, "User");
                    SignInManager.SignIn(user, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}