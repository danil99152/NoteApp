using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserRepository userRepository) : base(userRepository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}