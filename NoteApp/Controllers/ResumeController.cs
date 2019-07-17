using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteApp.Files;
using NoteApp.Models;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    [Authorize]
    public class ResumeController : BaseController
    {
        private readonly ResumeRepository resumeRepository;

        public ResumeController(ResumeRepository resumeRepository, IFileProvider[] fileProviders ,UserRepository userRepository) : 
            base(userRepository, fileProviders)
        {
            this.resumeRepository = resumeRepository;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ResumeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var files = new BinaryFile();
                string serverPath = Server.MapPath($"~/Uploaded Files/{ User.Identity.Name }/");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                if (model.Photo != null)
                {
                    string fileName = Path.GetFileName(model.Photo.FileName);
                    string filePath = Path.Combine(serverPath, fileName);
                    model.Photo.SaveAs(filePath);
                    files.Path = filePath;
                    files.Name = fileName;
                }
                var user = userRepository.GetCurrentUser(User);
                var resume = new Resume
                {
                    FIO = user,
                    PastPlaces = model.PastPlaces,
                    Requirments = model.Requirments,
                    Photo = files
                };
                resumeRepository.Save(resume);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public FileResult Download(string filePath, string fileName)
        {
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Delete(long noteId)
        {
            var resume = resumeRepository.Load(noteId);
            resumeRepository.Delete(resume);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long noteId)
        {
            var resume = resumeRepository.Load(noteId);
            var user = userRepository.GetCurrentUser(User);
            if (user.Equals(resume.FIO))
            {
                return PartialView("Details", resume);
            }
            return HttpNotFound();
        }

        public ActionResult Index(FetchOptions options)
        {
            var user = userRepository.GetCurrentUser(User);
            var resumes = resumeRepository.GetAllByUser(user, options);
            var model = new ResumeListViewModel
            {
                Resumes = resumes
            };
            return View(model);
        }
    }
}