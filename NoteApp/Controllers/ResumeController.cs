using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using NoteApp.BaseGenerator;
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

        [Authorize(Roles = "Candidate")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Candidate")]
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
                    Birthday = model.Birthday,
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

        [Authorize(Roles = "Candidate")]
        public ActionResult Delete(long resumeId)
        {
            var resume = resumeRepository.Load(resumeId);
            resumeRepository.Delete(resume);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(long resumeId)
        {
            var types = new List<SelectListItem>();

            Type generatorType = typeof(Generator);
            var assembliesUri = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string localPath = new Uri(assembliesUri).LocalPath;
            foreach (string file in Directory.EnumerateFiles(localPath, "*.dll"))
            {
                var member = Assembly.LoadFile(file).GetTypes().Where(type => type.BaseType.FullName == generatorType.FullName);

                foreach (var item in member)
                {
                    ConstructorInfo ci = item.GetConstructor(new Type[] { });
                    var Obj = ci.Invoke(new object[] { }) as Generator;
                    if (Obj.Name != null && item.FullName != null)
                    {
                        var selItem = new SelectListItem()
                        {
                            Text = Obj.Name,
                            Value = item.FullName
                        };

                        types.Add(selItem);
                    }

                }
                ViewBag.List = types;
            }
            var resume = resumeRepository.Load(resumeId);
            var user = userRepository.GetCurrentUser(User);
            if (user.Equals(resume.FIO))
            {
                return PartialView("Details", resume);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Details(Resume resume, Generator generator)
        {
            var fileUri = ConfigurationManager.AppSettings["uri"];
            var fileName = Guid.NewGuid() + $".{generator.Name}";
            string file = "";
            if (resume != null)
            {
                file = generator.Generate(resume, fileUri, fileName);
            }
            using (WebClient client = new WebClient())
            {
                client.DownloadData(file);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index(FetchOptions options)
        {
            var resumes = resumeRepository.GetAllResumes(options);
            var model = new ResumeListViewModel
            {
                Resumes = resumes
            };
            return View(model);
        }
    }
}