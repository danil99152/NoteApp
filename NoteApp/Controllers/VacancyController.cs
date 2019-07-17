using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteApp.Files;
using NoteApp.Models;
using NoteApp.Models.Models;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    [Authorize]
    public class VacancyController : BaseController
    {
        private readonly VacancyRepository vacancyRepository;
        public VacancyController(UserRepository userRepository, IFileProvider[] fileProviders, VacancyRepository vacancyRepository)
            : base(userRepository, fileProviders)
        {
            this.vacancyRepository = vacancyRepository;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VacancyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var vacancy = new Vacancy
                {
                    Name = model.Name,
                    Description = model.Description,
                    Time = model.Time,
                    Company = model.Company,
                    Requirments = model.Requirments,
                    Salary = model.Salary
                };
                vacancyRepository.Save(vacancy);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public FileResult Download(string filePath, string fileName)
        {
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Delete(long vacancyId)
        {
            var vacancy = vacancyRepository.Load(vacancyId);
            vacancyRepository.Delete(vacancy);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long vacancyId)
        {
            var vacancy = vacancyRepository.Load(vacancyId);
            return PartialView("Details", vacancy);
        }

        public ActionResult Index(FetchOptions options)
        {
            var user = userRepository.GetCurrentUser(User);
            var vacancies = vacancyRepository.GetAllByUser(user, options);
            var model = new VacancyListViewModel
            {
                Vacancies = vacancies
            };
            return View(model);
        }
    }
}