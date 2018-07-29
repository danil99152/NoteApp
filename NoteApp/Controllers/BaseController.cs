using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using NoteApp.Auth;
using NoteApp.Files;
using NoteApp.Models.Repositories;
using NoteApp.Permission;

namespace NoteApp.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;
        protected IFileProvider[] fileProviders;

        public BaseController(UserRepository userRepository, IFileProvider[] fileProviders)
        {
            this.userRepository = userRepository;
            this.fileProviders = fileProviders;
        }

        public SignInManager SignInManager
            => HttpContext.GetOwinContext().Get<SignInManager>();

        public UserManager UserManager
            => HttpContext.GetOwinContext().GetUserManager<UserManager>();

        public RoleManager RoleManager
            => HttpContext.GetOwinContext().Get<RoleManager>();

        public IFileProvider GetFileProvider()
        {
            var key = ConfigurationManager.AppSettings["FileProvider"];
            return fileProviders.First(p => p.Name == key);
        }
    }
}