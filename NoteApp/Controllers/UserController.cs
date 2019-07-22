using Microsoft.AspNet.Identity;
using NoteApp.Files;
using NoteApp.Models;
using NoteApp.Models.Models;
using NoteApp.Models.Repositories;
using System.Web.Mvc;

namespace NoteApp.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(UserRepository userRepository, IFileProvider[] fileProviders) : 
            base(userRepository, fileProviders)
        {
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Manage(FetchOptions options)
        {
            var model = new UserListViewModel
            {
                Users = userRepository.GetAll(options)
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult LockUnlock(long userId)
        {
            var user = userRepository.Load(userId);
            user.IsEnabled = !user.IsEnabled;
            userRepository.Update(user);
            return RedirectToAction("Manage");
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new UserViewModel { Entity = new User() };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = UserManager.CreateAsync(model.Entity, model.Password);
                if (res.Result == IdentityResult.Success)
                {
                    return RedirectToAction("Manage");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long Id)
        {
            var user = userRepository.Load(Id);
            userRepository.Delete(user);
            return RedirectToAction("Manage");
        }
    }
}